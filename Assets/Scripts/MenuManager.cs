using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public bool isLostScene;
    public AudioSource scream;

    private void Start()
    {
        if (isLostScene)
            scream.Play();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("TestScene");
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
