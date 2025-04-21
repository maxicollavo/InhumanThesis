using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    [SerializeField] GameObject optionMenu;
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
        optionMenu.SetActive(true);
    }

    public void BackToMenu()
    {
        optionMenu.SetActive(false);
    }

}
