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
    bool isDecreasing;
    float counter = 5f;

    #region Sounds
    [SerializeField] AudioSource timerSound;
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
            counterText.color = new Color(counterText.color.r, counterText.color.g, counterText.color.b, 0);
            StopAllCoroutines();
            OpenDoor();
            part.Stop();
        }

        if (isDecreasing && counter > 0)
        {
            counter -= Time.deltaTime;
        }
    }

    public void Interact()
    {
        timerSound.Play();
        part.Play();
        anim.SetBool("OnAction", true);

    }

    void RestartSparkle()
    {
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
        isDecreasing = true;
        //Algun feedback del timer
        yield return new WaitForSeconds(5f);
        RestartSparkle();
        yield break;
    }

    public void StopCounterCoroutine()
    {
        StopCoroutine(DecreaseCableCounterAfterDelay());
    }
}
