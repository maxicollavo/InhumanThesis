using UnityEngine;

public class DoorCloser : MonoBehaviour, ICloser
{
    [SerializeField] Animator doorAnim;
    [SerializeField] Animator doorAnimTwo;
    [SerializeField] Animator clockAnim;

    [SerializeField] AudioSource closeSound;

    public bool isActivated;
    public bool isDimension;
    public bool countsForTP;

    public bool isTorchShutting;

    public void Close()
    {
        var boxColl = GetComponent<BoxCollider>();

        if (!isActivated)
        {
            closeSound.Play();
        }

        if (isDimension)
            GameManager.Instance.ableToTeleport = true;

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

        clockAnim.SetBool("IsTrue", true);

        if (doorAnim.GetBool("IsTrue"))
            doorAnim.SetBool("IsTrue", false);

        if (doorAnimTwo.GetBool("IsTrue"))
            doorAnimTwo.SetBool("IsTrue", false);
    }
}
