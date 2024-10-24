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
    public bool countsForTP;

    public bool isTorchShutting;

    public void Close()
    {
        var boxColl = GetComponent<BoxCollider>();

        if (!isActivated)
        {
            closeSound.Play();
            GameManager.Instance.StartLevelTimer();
        }

        if (isFirst)
            timerGO.SetActive(true);

        if (isDimension)
            GameManager.Instance.ableToTeleport = true;
             Debug.Log("esta en el otro mundo");

        if (isTorchShutting)
        {
            GameManager.Instance.TorchSoundStop();
        }

        if (countsForTP)
        {
            LaserBeam.Instance.tpCounter++;
            boxColl.enabled = false;
        }

        isActivated = true;


        if (doorAnim.GetBool("IsTrue"))
            doorAnim.SetBool("IsTrue", false);

        if (doorAnimTwo.GetBool("IsTrue"))
            doorAnimTwo.SetBool("IsTrue", false);
    }
}
