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
    [SerializeField] GameObject powerWheel;
    #endregion GameObjects

    #region Animators
    [SerializeField] Animator doorTorch;
    [SerializeField] Animator doorTorchTwo;

    [SerializeField] Animator doorPaint;
    [SerializeField] Animator doorPaintTwo;
    #endregion Animators

    #region Bools
    [HideInInspector]
    public bool electricityIsRunning;
    private bool menuPressed;
    [HideInInspector]
    public bool canShoot;
    [HideInInspector]
    public bool ableToTeleport;
    [HideInInspector]
    public bool allCablesArrived;
    private bool activatingWheel;
    #endregion Bools

    #region Sounds
    [SerializeField] AudioSource doorOpenSound;
    [SerializeField] AudioSource codeSound;
    [SerializeField] AudioSource paintSound;
    [SerializeField] AudioSource explosionSound;
    [SerializeField] AudioSource first30Secs;
    [SerializeField] AudioSource last30Secs;
    #endregion Sounds

    #region Lists
    [HideInInspector]
    public List<GameObject> torches = new List<GameObject>();
    [HideInInspector]
    public List<bool> torchsLit;

    public List<Transform> spawnerUpside = new List<Transform>();
    public List<Transform> spawnerReal = new List<Transform>();


    public List<bool> cablesStatus = new List<bool> { false, false };

    public List<bool> paintings = new List<bool>();

    public List<bool> rail = new List<bool>();
    #endregion Lists

    #region HeartBeat UI
    public RectTransform heart;
    private Vector3 heartSize;
    public float initialBeatScale = 0.8f;
    public float initialBeatDuration = 0.5f;
    public float acceleratedBeatDuration = 0.25f;
    #endregion HeartBeat UI

    #region PowerSwitching
    private int powerInt;
    private int maxPowerInt;
    #endregion PowerSwitching

    public BoxCollider torchButton;
    public BoxCollider cableButton;
    public BoxCollider paintButton;
    public BoxCollider colorButton;
    public BoxCollider railButton;

    public AudioSource winBell;

    public float levelCounter = 60;

    public static GameManager Instance { get; set; }

    private void Awake()
    {
        Instance = this;
        torchsLit = new List<bool>();
        paintSound.Stop();
    }

    private void Start()
    {
        maxPowerInt = 1;
        codeCount = 0;
        canShoot = true;

        heartSize = new Vector3(heart.transform.localScale.x, heart.transform.localScale.y, heart.transform.localScale.z);
    }

    public void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll > 0f)
        {
            if (powerInt >= maxPowerInt || !ableToTeleport) return;
            powerInt += 1;

            PowerChange.Instance.PowerChangeCall(powerInt);
        }
        else if (scroll < 0f)
        {
            if (powerInt > 0)
            {
                powerInt -= 1;
                PowerChange.Instance.PowerChangeCall(powerInt);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuPressed = !menuPressed;
            pauseMenu.SetActive(menuPressed);

            if (menuPressed)
            {
                canShoot = false;
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                canShoot = true;
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        if (levelCounter <= 0)
        {
            SceneManager.LoadScene("LostScene");
        }
    }

    public void ChangeState(PowerStates power)
    {
        state = power;
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
        torchButton.enabled = true;
        winBell.Play();
        doorOpenSound.Play();
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
        cableButton.enabled = true;
        winBell.Play();
        allCablesArrived = true;
        explosionSound.Play();
        yield return new WaitForSeconds(1.5f);
        doorOpenSound.Play();

        CoroutinesStoper();
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
        paintButton.enabled = true;
        winBell.Play();
        CoroutinesStoper();

        doorPaint.SetBool("IsTrue", true);
        doorPaintTwo.SetBool("IsTrue", true);
    }
    #endregion PaintPuzzle

    #endregion Puzzles

    public void CoroutinesStoper()
    {
        StopAllCoroutines();
        LeanTween.cancel(heart);

        first30Secs.Stop();
        last30Secs.Stop();
    }

    public void StartLevelTimer()
    {
        heart.localScale = heartSize;
        levelCounter = 60;
        StartCoroutine(DecreaseLevelTime());
    }

    public IEnumerator DecreaseLevelTime()
    {
        first30Secs.Play(); bool last30 = false;

        StartHeartBeat(initialBeatDuration);

        while (levelCounter > 0)
        {
            yield return new WaitForSeconds(1f);
            levelCounter--;

            if (levelCounter <= 30 && !last30)
            {
                last30Secs.Play();
                last30 = true;
                StartHeartBeat(acceleratedBeatDuration);
            }
        }
    }

    void StartHeartBeat(float beatDuration)
    {
        LeanTween.cancel(heart);

        LeanTween.scale(heart, new Vector3(initialBeatScale, initialBeatScale, initialBeatScale), beatDuration)
                 .setEase(LeanTweenType.easeInOutSine)
                 .setLoopPingPong();
    }

    public void TorchSoundStop()
    {
        foreach (var torch in torches)
        {
            var sound = torch.GetComponent<CodeInteractor>();
            sound.fireSound.Stop();
        }
    }
}

public enum PowerStates
{
    OnLaser,
    OnDimension
}