using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region States
    [HideInInspector]
    public PowerStates state;
    #endregion States

    #region Ints
    public int codeCount;
    public int cableCounter;
    public int paintCounter;
    #endregion Ints

    #region GameObjects
    [SerializeField] GameObject secretCode;
    [SerializeField] GameObject doorToOpen;
    [SerializeField] GameObject paintingsDoor;
    [SerializeField] GameObject pauseMenu;
    #endregion GameObjects

    #region Animators
    [SerializeField] Animator doorTorch;
    [SerializeField] Animator doorTorchTwo;
    #endregion Animators

    #region Bools
    public bool electricityIsRunning;
    public bool menuPressed;
    public bool canPlaySound;
    [HideInInspector]
    public bool allCablesArrived;
    #endregion Bools

    #region Sounds
    [SerializeField] AudioSource doorOpenSound;
    [SerializeField] AudioSource codeSound;
    [SerializeField] AudioSource paintSound;
    [SerializeField] AudioSource explosionSound;
    [SerializeField] AudioSource horrorSound;
    [SerializeField] AudioSource TransitionHorrorDoor;
    #endregion Sounds

    #region Lists
    [HideInInspector]
    public List<GameObject> torches = new List<GameObject>();
    [HideInInspector]
    public List<bool> torchsLit;
    public List<bool> paintings = new List<bool>();
    public List<bool> cablesStatus = new List<bool> { false, false };
    #endregion Lists

    public static GameManager Instance { get; set; }

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

    #region Puzzles
    #region TorchPuzzle
    private void ShowAndHideCode(bool areTorchLit)
    {
        secretCode.SetActive(areTorchLit);

        if (areTorchLit)
        {
            codeSound.Play();
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

        var torchesWithIndex = GetTorchesLitGenerator()
        .Select((isLit, index) => new { Index = index, IsLit = isLit })
        .ToList();

        bool allTorchsLit = torchesWithIndex
            .Aggregate(true, (allLit, torch) => allLit && torch.IsLit);

        ShowAndHideCode(allTorchsLit);

        bool requiredTorches = torchesWithIndex
        .Where(t => t.Index == 0 || t.Index == 4 || t.Index == 5)
        .Aggregate(true, (result, torch) => result && torch.IsLit);

        bool notRequiredTorches = torchesWithIndex
        .Where(t => t.Index == 1 || t.Index == 2 || t.Index == 3)
        .Aggregate(true, (result, torch) => result && !torch.IsLit);

        if (requiredTorches && notRequiredTorches)
        {
            OpenTorchDoor();
        }
    }

    public IEnumerable<bool> GetTorchesLitGenerator()
    {
        foreach (var state in torchsLit)
        {
            yield return state;
        }
    }

    private void OpenTorchDoor()
    {
        doorOpenSound.Play();
        TransitionHorrorDoor.Play();
        doorTorch.SetBool("IsTrue", true);
        doorTorchTwo.SetBool("IsTrue", true);
    }
    #endregion TorchPuzzle

    #region CablePuzzle
    public void UpdateCableStatus(int index, bool value)
    {
        if (index >= 0 && index < cablesStatus.Count)
            cablesStatus[index] = value;

        if (cablesStatus.Zip(cablesStatus, (first, second) => first && second).All(isConnected => isConnected))
        {
            StartCoroutine(OpenCableDoor());
        }
    }

    private IEnumerator OpenCableDoor()
    {
        allCablesArrived = true;

        explosionSound.Play();
        yield return new WaitForSeconds(3f);
        doorOpenSound.Play();
    }
    #endregion CablePuzzle

    #region PaintPuzzle
    public void UpdatePaintings(int index, bool value)
    {
        paintSound.Play();

        if (index >= 0 && index < paintings.Count)
            paintings[index] = value;

        if (paintings.All(p => p))
        {
            WinPaintPuzzle();
        }
    }

    public void WinPaintPuzzle()
    {
        SceneManager.LoadScene("EndDemoScene");
        horrorSound.Play();
    }
    #endregion PaintPuzzle
    #endregion Puzzles
}

public enum PowerStates
{
    OnLaser
}