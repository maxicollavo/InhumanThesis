using UnityEngine;

public class RailManager : MonoBehaviour
{
    [SerializeField] Animator rightDoor;
    [SerializeField] Animator leftDoor;
    [SerializeField] Animator clockAnim;
    public AudioSource winBell;

    private int counter;
    public void CheckWin()
    {
        foreach (var item in GameManager.Instance.rail)
        {
            if (item)
            {
                counter++;

                if (counter == 3)
                {
                    GameManager.Instance.railButton.enabled = true;
                    rightDoor.SetBool("IsTrue", true);
                    leftDoor.SetBool("IsTrue", true);
                    clockAnim.speed = 0;
                    winBell.Play();
                }
            }
            else
            {
                counter = 0;
                return;
            }
        }
    }
}
