using UnityEngine;

public class DoorCloser : MonoBehaviour, ICloser
{
    [SerializeField] Animator doorAnim;
    [SerializeField] Animator doorAnimTwo;

    [SerializeField] GameObject timerGO;

    [SerializeField] AudioSource closeSound;

    public bool isActivated;
    public bool isFirst;
    public bool isDimension;

    public bool isTorchShutting;

    public void Close()
    {
        if (!isActivated)
        {
            closeSound.Play();
            GameManager.Instance.StartLevelTimer();
        }

        if (isFirst)
            timerGO.SetActive(true);

        if (isDimension)
            GameManager.Instance.ableToTeleport = true;

        if (isTorchShutting)
        {
            GameManager.Instance.TorchSoundStop();
        }

        isActivated = true;


        if (doorAnim.GetBool("IsTrue"))
            doorAnim.SetBool("IsTrue", false);

        if (doorAnimTwo.GetBool("IsTrue"))
            doorAnimTwo.SetBool("IsTrue", false);
    }
}
