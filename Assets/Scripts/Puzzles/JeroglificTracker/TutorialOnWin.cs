using UnityEngine;

public class TutorialOnWin : MonoBehaviour
{
    [SerializeField] TrailManager manager;

    [SerializeField] Animator doorAnim;

    void Start()
    {
        manager.JeroglificAction += Win;
    }

    void Win(TrailManager manager)
    {
        doorAnim.SetTrigger("Open");
    }
}
