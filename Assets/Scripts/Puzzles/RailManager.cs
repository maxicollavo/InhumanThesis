using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RailManager : MonoBehaviour
{
    [SerializeField] Animator rightWinDoorLvlOne;
    [SerializeField] Animator leftWinDoorLvlOne;
    public AudioSource winBell;

    [SerializeField] CameraShake camShake;

    private int counter;
    private bool hasWon;

    public static RailManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void CheckWin()
    {
        if (hasWon) return;

        foreach (var item in GameManager.Instance.rail)
        {
            if (item)
            {
                counter++;

                if (counter == 4)
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
        hasWon = true;
        Timer.Instance.hasWon = true;
        winBell.Play();
        camShake.TriggerShake(2f);
        yield return new WaitForSeconds(2f);
        rightWinDoorLvlOne.SetBool("IsTrue", true);
        leftWinDoorLvlOne.SetBool("IsTrue", true);
    }
}
