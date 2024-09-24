using System.Collections;
using TMPro;
using UnityEngine;

public class CableInteractor : MonoBehaviour, Interactor
{
    [SerializeField] Animator anim;
    [SerializeField] Animator door;
    [SerializeField] Animator door2;
    [SerializeField] ParticleSystem part;
    [SerializeField] BoxCollider collider;
    [SerializeField] TextMeshProUGUI counterText;
    [SerializeField] GameObject LightGO;
    bool isDecreasing;
    float counter = 5f;

    #region Sounds
    [SerializeField] AudioSource timerSound;
    [SerializeField] AudioSource electricSound;
    [SerializeField] AudioSource openDoorSound;
    #endregion Sounds

    private void Start()
    {
        counterText.color = new Color(counterText.color.r, counterText.color.g, counterText.color.b, 0);
    }

    private void Update()
    {
        counterText.text = Mathf.FloorToInt(counter).ToString();
        Debug.Log(GameManager.Instance.cableCounter);

        if (GameManager.Instance.cableCounter == 2)
        {
            StartCoroutine(PlayElectricExplosion());
            counterText.color = new Color(counterText.color.r, counterText.color.g, counterText.color.b, 0);
            electricSound.Stop();
            part.Stop();
        }

        if (isDecreasing && counter > 0)
        {
            counter -= Time.deltaTime;
        }
    }

    public void Interact()
    {
        part.Play();
        electricSound.Play();
        anim.SetBool("OnAction", true);
    }

    void RestartSparkle()
    {
        electricSound.Stop();
        counterText.color = new Color(counterText.color.r, counterText.color.g, counterText.color.b, 0);
        isDecreasing = false;
        counter = 5f;
        anim.SetBool("OnAction", false);
        anim.Play("OnAction", -1, 0f);
        part.Stop();
        collider.enabled = true;
        GameManager.Instance.cableCounter--;
    }

    void OpenDoor()
    {
        Debug.Log("OpenDoor method called");
        door.SetBool("IsTrue", true);
        door2.SetBool("IsTrue", true);
    }

    public void AnimFinish()
    {
        GameManager.Instance.cableCounter++;
        counterText.color = new Color(counterText.color.r, counterText.color.g, counterText.color.b, 1);

        StartCoroutine(DecreaseCableCounterAfterDelay());
    }

    private IEnumerator DecreaseCableCounterAfterDelay()
    {
        GameManager.Instance.electricityIsRunning = true;

        if (!GameManager.Instance.electricityIsRunning)
        {
            isDecreasing = true;

            while (counter > 0)
            {
                timerSound.Play();
                yield return new WaitForSeconds(1f);
                counter--;
            }

            RestartSparkle();
            isDecreasing = false;
        }
    }

    private IEnumerator PlayElectricExplosion()
    {
        openDoorSound.Play();
        LightGO.SetActive(false);
        yield return new WaitForSeconds(1.9f);
        OpenDoor();
    }
}
