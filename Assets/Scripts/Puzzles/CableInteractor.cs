using System.Collections;
using UnityEngine;

public class CableInteractor : MonoBehaviour, Interactor
{
    [SerializeField] Animator anim;
    [SerializeField] Animator door;
    [SerializeField] Animator door2;
    [SerializeField] Animator clockAnim;
    [SerializeField] Animator LightFlash;
    [SerializeField] Animator LightFlash2;
    [SerializeField] ParticleSystem electricParticle;
    [SerializeField] BoxCollider collider;
    [SerializeField] GameObject LightGO;
    [SerializeField] GameObject LightGO2;
    [SerializeField] GameObject LightGO3;
    [SerializeField] int index;

    private bool isActivated;
    private bool hasDone;

    [SerializeField] Renderer lightIndicatorMat;
    [SerializeField] Renderer electricBoxLightMat;
    [SerializeField] Material green;
    [SerializeField] Material red;

    [SerializeField] Material railRed;
    [SerializeField] Material railBlue;
    [SerializeField] Material railYellow;
    [SerializeField] Material railGreen;
    float counter = 5f;

    #region Sounds
    [SerializeField] AudioSource timerSound;
    [SerializeField] AudioSource electricSound;
    #endregion Sounds

    private void Update()
    {
        if (GameManager.Instance.allCablesArrived && !hasDone)
        {
            isActivated = true;
            lightIndicatorMat.material = green;
            electricBoxLightMat.material = green;
            StopCoroutine(DecreaseCableCounterAfterDelay());
            timerSound.Stop();
            electricSound.Stop();
            electricParticle.Stop();
            TurnRailOn();
            GameManager.Instance.cameraShake.TriggerShake();
        }
    }

    public void Interact()
    {
        if (!isActivated)
        {
            isActivated = true;
            electricParticle.Play();
            electricSound.Play();
            anim.SetBool("OnAction", true);
        }
    }

    void RestartSparkle()
    {
        GameManager.Instance.electricityIsRunning = false;
        GameManager.Instance.cableCounter--;
        lightIndicatorMat.material = red;
        electricBoxLightMat.material = red;
        electricParticle.Stop();
        anim.SetBool("OnAction", false);
        anim.Play("OnAction", -1, 0f);
        counter = 5f;
        electricSound.Stop();
        isActivated = false;
        collider.enabled = true;
    }

    void TurnRailOn()
    {
        //clockAnim.speed = 0;
        foreach (var redRail in GameManager.Instance.redRailList)
        {
            var renderer = redRail.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material = railRed;
            }
        }
        foreach (var greenRail in GameManager.Instance.greenRailList)
        {
            var renderer = greenRail.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material = railGreen;
            }
        }
        foreach (var yellowRail in GameManager.Instance.yellowRailList)
        {
            var renderer = yellowRail.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material = railYellow;
            }
        }
        foreach (var blueRail in GameManager.Instance.blueRailList)
        {
            var renderer = blueRail.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material = railBlue;
            }
        }
        foreach (var button in GameManager.Instance.railButtons)
        {
            var coll = button.GetComponent<BoxCollider>();
            coll.enabled = true;
        }
        hasDone = true;
    }

    public void AnimFinish()
    {
        GameManager.Instance.cableCounter++;
        LightFlash.SetBool("LightIsTrue", true);
        LightFlash2.SetBool("LightIsTrue", true);
        lightIndicatorMat.material = green;
        electricBoxLightMat.material = green;

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
                if (GameManager.Instance.allCablesArrived) break;

                timerSound.Play();
                yield return new WaitForSeconds(1f);
                counter--;
            }

            if (!GameManager.Instance.allCablesArrived)
            {
                RestartSparkle();
            }
        }
    }
}