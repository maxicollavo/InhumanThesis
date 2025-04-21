using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public bool OnTest;

    private void Update()
    {
        if (OnTest)
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                SceneManager.LoadScene("LevelOnePruebas");
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.F2))
            {
                SceneManager.LoadScene("TestScene");
            }
        }
    }
    public void Exit()
    {
        Application.Quit();
    }

    public void StartScene()
    {
        SceneManager.LoadScene("Level_One");
    }

    public void Options()
    {
        SceneManager.LoadScene("Options");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

}
