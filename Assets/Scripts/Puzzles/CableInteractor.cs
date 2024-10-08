using System.Collections;
using TMPro;
using UnityEngine;

public class CableInteractor : MonoBehaviour, Interactor
{
    [SerializeField] Animator anim;
    [SerializeField] Animator door;
    [SerializeField] Animator door2;
    [SerializeField] ParticleSystem electricParticle;
    [SerializeField] BoxCollider collider;
    [SerializeField] TextMeshProUGUI counterText;
    [SerializeField] GameObject LightGO;
    [SerializeField] int index;

    [SerializeField] Renderer lightIndicatorMat;
    [SerializeField] Material green;
    [SerializeField] Material red;
    float counter = 5f;

    #region Sounds
    [SerializeField] AudioSource timerSound;
    [SerializeField] AudioSource electricSound;
    #endregion Sounds

    private void Start()
    {
        counterText.color = new Color(counterText.color.r, counterText.color.g, counterText.color.b, 0);
    }

    private void Update()
    {
        counterText.text = Mathf.FloorToInt(counter).ToString();

        if (GameManager.Instance.allCablesArrived)
        {
            lightIndicatorMat.material = green;
            StopCoroutine(DecreaseCableCounterAfterDelay());
            timerSound.Stop();
            electricSound.Stop();
            counterText.color = new Color(counterText.color.r, counterText.color.g, counterText.color.b, 0);
            electricParticle.Stop();

            StartCoroutine(PlayElectricExplosion());
        }
    }

    public void Interact()
    {
        electricParticle.Play();
        electricSound.Play();
        anim.SetBool("OnAction", true);
    }

    void RestartSparkle()
    {
        GameManager.Instance.electricityIsRunning = false;
        lightIndicatorMat.material = red;
        electricSound.Stop();
        counterText.color = new Color(counterText.color.r, counterText.color.g, counterText.color.b, 0);
        counter = 5f;
        anim.SetBool("OnAction", false);
        anim.Play("OnAction", -1, 0f);
        electricParticle.Stop();
        collider.enabled = true;
        GameManager.Instance.cableCounter--;
    }

    void OpenDoor()
    {
        door.SetBool("IsTrue", true);
        door2.SetBool("IsTrue", true);
    }

    public void AnimFinish()
    {
        GameManager.Instance.cableCounter++;
        lightIndicatorMat.material = green;
        counterText.color = new Color(counterText.color.r, counterText.color.g, counterText.color.b, 1);

        GameManager.Instance.UpdateCableStatus(GameManager.Instance.cableCounter - 1, true);

        StartCoroutine(DecreaseCableCounterAfterDelay());
    }

    private IEnumerator DecreaseCableCounterAfterDelay()
    {
        if (!GameManager.Instance.electricityIsRunning)
        {
            GameManager.Instance.electricityIsRunning = true;

            while (counter > 0)
            {
                timerSound.Play();
                yield return new WaitForSeconds(1f);
                counter--;
            }

            RestartSparkle();
        }
    }

    private IEnumerator PlayElectricExplosion()
    {
        LightGO.SetActive(false);
        yield return new WaitForSeconds(3f);
        OpenDoor();
    }
}