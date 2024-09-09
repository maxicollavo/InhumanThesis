using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public PowerStates state;
    public List<GameObject> torches = new List<GameObject>();
    [HideInInspector]
    public List<bool> torchsLit;
    [HideInInspector]
    public int codeCount;
    public int cableCounter;
    [SerializeField]
    GameObject secretCode;
    [SerializeField]
    GameObject doorToOpen;
    [SerializeField] GameObject pauseMenu;

    public bool menuPressed;
    public static GameManager Instance { get; set; }

    private void Awake()
    {
        Instance = this;
        torchsLit = new List<bool>();
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

        if (allTorchsLit)
        {
            secretCode.SetActive(true);
        }
        else
        {
            secretCode.SetActive(false);
        }

        if (torchsLit[0] && torchsLit[4] && torchsLit[5] && !torchsLit[1] && !torchsLit[2] && !torchsLit[3])
        {
            doorToOpen.SetActive(false);
        }
    }
}

public enum PowerStates
{
    OnLaser,
}
