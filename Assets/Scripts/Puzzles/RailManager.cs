using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RailManager : MonoBehaviour
{
    [SerializeField] Animator rightWinDoorLvlOne;
    [SerializeField] Animator leftWinDoorLvlOne;
    public AudioSource winBell;

    private int counter;

    public static RailManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

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
        Timer.Instance.hasWon = true;
        winBell.Play();
        yield return new WaitForSeconds(2f);
        rightWinDoorLvlOne.SetBool("IsTrue", true);
        leftWinDoorLvlOne.SetBool("IsTrue", true);
    }
}
