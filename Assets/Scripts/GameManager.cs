using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public PowerStates state;
    public List<GameObject> torches = new List<GameObject>();
    public List<bool> paintings = new List<bool>();
    [HideInInspector]
    public List<bool> torchsLit;
    [HideInInspector]
    public int codeCount;
    public int cableCounter;
    public int paintCounter;
    public bool electricityIsRunning;
    bool allTorchsLit = true;
    [SerializeField]
    GameObject secretCode;
    [SerializeField]
    GameObject doorToOpen;
    [SerializeField]
    GameObject paintingsDoor;
    [SerializeField] GameObject pauseMenu;
    public bool menuPressed;
    public bool canPlaySound;
    public static GameManager Instance { get; set; }

    [SerializeField] Animator doorTorch;
    [SerializeField] Animator doorTorchTwo;
    [SerializeField] AudioSource doorOpenSound;
    [SerializeField] AudioSource codeSound;
    [SerializeField] AudioSource paintSound;

    private void Awake()
    {
        Instance = this;
        torchsLit = new List<bool>();
        paintSound.Stop();
    }

    private void Start()
    {
        codeCount = 0;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuPressed = !menuPressed;
            pauseMenu.SetActive(menuPressed);
            if (menuPressed)
            {
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    private void OpenTorchDoor()
    {
        doorOpenSound.Play();
        doorTorch.SetBool("IsTrue", true);
        doorTorchTwo.SetBool("IsTrue", true);
    }

    private void ShowAndHideCode(bool areTorchLit)
    {
        secretCode.SetActive(areTorchLit);

        if (areTorchLit)
        {
            codeSound.Play();
        }
    }

    public void UpdatePaintings(int index, bool value)
    {
        paintSound.Play();

        if (index >= 0 && index < paintings.Count)
            paintings[index] = value;

        foreach (var item in paintings)
        {
            if (!item)
            {
                paintCounter = 0;
                return;
            }

            paintCounter++;
            if (paintCounter == 4)
            {
                if (paintingsDoor.activeInHierarchy) SceneManager.LoadScene("EndDemoScene");
            }
        }
    }

    public void GetAllTorchs()
    {
        if (torchsLit.Count != torches.Count)
        {
            torchsLit = new List<bool>(new bool[torches.Count]);
        }

        for (int i = 0; i < torches.Count; i++)
        {
            torchsLit[i] = torches[i].GetComponent<CodeInteractor>().isLit;
        }

        bool allTorchsLit = true;
        for (int i = 0; i < torchsLit.Count; i++)
        {
            if (!torchsLit[i])
            {
                allTorchsLit = false;
                break;
            }
        }

        ShowAndHideCode(allTorchsLit);

        if (torchsLit[0] && torchsLit[4] && torchsLit[5] && !torchsLit[1] && !torchsLit[2] && !torchsLit[3])
        {
            OpenTorchDoor();
        }
    }
}

public enum PowerStates
{
    OnLaser,
}
