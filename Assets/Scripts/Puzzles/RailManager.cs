using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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
                    StartCoroutine(Win());
                }
            }
            else
            {
                counter = 0;
                return;
            }
        }
    }

    private IEnumerator Win()
    {
        //GameManager.Instance.railButton.enabled = true;
        //rightDoor.SetBool("IsTrue", true);
        //leftDoor.SetBool("IsTrue", true);
        Debug.Log("Se gana");
        clockAnim.speed = 0;
        winBell.Play();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("EndDemoScene");
    }
}
