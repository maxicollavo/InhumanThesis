using UnityEngine;

public class DoorCloser : MonoBehaviour, ICloser
{
    [SerializeField] Animator doorAnim;
    [SerializeField] Animator doorAnimTwo;

    [SerializeField] GameObject timerGO;

    public bool isActivated;
    public bool isFirst;

    public void Close()
    {
        if (!isActivated)
            GameManager.Instance.StartLevelTimer();

        if (isFirst)
            timerGO.SetActive(true);

        isActivated = true;

        if (doorAnim.GetBool("IsTrue"))
            doorAnim.SetBool("IsTrue", false);

        if (doorAnimTwo.GetBool("IsTrue"))
            doorAnimTwo.SetBool("IsTrue", false);
    }
}
