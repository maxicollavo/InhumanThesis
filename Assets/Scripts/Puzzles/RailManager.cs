using UnityEngine;

public class RailManager : MonoBehaviour
{
    [SerializeField] Animator rightDoor;
    [SerializeField] Animator leftDoor;
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
                    GameManager.Instance.CoroutinesStoper();
                    GameManager.Instance.railButton.enabled = true;
                    rightDoor.SetBool("IsTrue", true);
                    leftDoor.SetBool("IsTrue", true);
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
