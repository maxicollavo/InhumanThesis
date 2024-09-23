using UnityEngine;

public class SwitchTurnOff : MonoBehaviour, Interactor
{
    public Animator Anim;
    public Animator Anim2;

    private void Start()
    {
        Anim2.SetBool("IsTrue", false);
    }

    public void Interact()
    {
        OpenDoor();
    }

    public void OpenDoor()
    {
        Anim.SetBool("IsTrue", true);
        Anim2.SetBool("IsTrue", true);
    }
}


