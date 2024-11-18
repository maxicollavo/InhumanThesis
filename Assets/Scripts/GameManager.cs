using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public bool canMove = true;

    [SerializeField]
    Transform jumpscare;

    [SerializeField]
    Transform jumpscareUpside;

    [SerializeField]
    ScreamerTeleport realScreamer;

    [SerializeField]
    ScreamerTeleport upsideScreamer;

    private Transform cameraTransform;

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
    [SerializeField] GameObject torchSecretCode;
    [SerializeField] GameObject railSecretCode;
    [SerializeField] GameObject doorToOpen;
    [SerializeField] GameObject paintingsDoor;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject powerWheel;
    #endregion GameObjects

    #region Animators
    [SerializeField] Animator clockAnim;

    [SerializeField] Animator firstPuzzleRealLeftDoor;
    [SerializeField] Animator firstPuzzleRealRightDoor;
    [SerializeField] Animator firstPuzzleUpsideLeftDoor;
    [SerializeField] Animator firstPuzzleUpsideRightDoor;
    #endregion Animators

    #region Bools
    [HideInInspector]
    public bool electricityIsRunning;
    private bool menuPressed;
    [HideInInspector]
    public bool canShoot;
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
    [SerializeField] AudioSource firstBeats;
    [SerializeField] AudioSource last30Secs;
    [SerializeField] AudioSource mirror;
    [SerializeField] AudioSource jumpscareSound;
    #endregion Sounds

    #region Lists
    public List<GameObject> torches = new List<GameObject>();
    [HideInInspector]
    public List<bool> torchsLit;
    public List<GameObject> redRailList;
    public List<GameObject> blueRailList;
    public List<GameObject> yellowRailList;
    public List<GameObject> greenRailList;
    public List<GameObject> railButtons;

    public List<GameObject> pumpkins;

    public List<Transform> spawnerUpside = new List<Transform>();
    public List<Transform> spawnerReal = new List<Transform>();


    public List<bool> cablesStatus = new List<bool> { false, false };

    public List<bool> paintings = new List<bool>();

    public List<bool> rail = new List<bool>();
    #endregion Lists

    #region PowerSwitching
    private int powerInt;
    private int maxPowerInt;
    #endregion PowerSwitching

    public BoxCollider torchButton;
    public BoxCollider cableButton;
    public BoxCollider paintButton;
    public BoxCollider colorButton;

    public AudioSource winBell;

    public PostProcessProfile profile;
    private UnityEngine.Rendering.PostProcessing.ChromaticAberration ca;
    private UnityEngine.Rendering.PostProcessing.ColorGrading cg;

    public CameraShake cameraShake;

    public static GameManager Instance { get; set; }

    private void Awake()
    {
        Instance = this;
        paintSound.Stop();
    }

    private void Start()
    {
        cameraTransform = Camera.main.transform;

        ca = profile.GetSetting<UnityEngine.Rendering.PostProcessing.ChromaticAberration>();
        cg = profile.GetSetting<UnityEngine.Rendering.PostProcessing.ColorGrading>();
        ca.intensity.Override(0);
        cg.active = false;
        maxPowerInt = 1;
        codeCount = 0;
        canShoot = true;
    }

    public void Update()
    {
        if (LaserBeam.Instance.playerOnUpside)
        {
            ca.intensity.Override(1);
            cg.active = true;
        }
        else
        {
            ca.intensity.Override(0);
            cg.active = false;
        }

        if (!canMove)
        {
            Debug.Log("Se detiene el GameManager");
            return;
        }

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

        if (Timer.Instance.remainingTime <= 1)
        {
            Debug.Log("empieza la corutina de mirar al bicho");
            if (!LaserBeam.Instance.playerOnUpside)
            {
                StartCoroutine(JumpscareLookAt(jumpscare));
            }
            else
            {
                StartCoroutine(JumpscareLookAt(jumpscareUpside));
            }
        }
    }

    public void ChangeState(PowerStates power)
    {
        state = power;
    }

    #region Puzzles

    #region TorchPuzzle
    private void ShowAndHideSecretCode(bool areTorchLit, GameObject secretCode)
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

        ShowAndHideSecretCode(allTorchsLit, torchSecretCode);

        bool requiredTorches = torchesWithIndex
                                .Where(t => t.Index == 0 || t.Index == 4 || t.Index == 5)
                                .Aggregate(true, (result, torch) => result && torch.IsLit);

        bool notRequiredTorches = torchesWithIndex
                                .Where(t => t.Index == 1 || t.Index == 2 || t.Index == 3)
                                .Aggregate(true, (result, torch) => result && !torch.IsLit);

        if (requiredTorches && notRequiredTorches)
        {
            ShowAndHideSecretCode(true, railSecretCode);
        }
        else
        {
            ShowAndHideSecretCode(false, railSecretCode);
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
    }
    #endregion TorchPuzzle

    #region CablePuzzle
    public void UpdateCableStatus(int index, bool value)
    {
        if (index >= 0 && index < cablesStatus.Count)
            cablesStatus[index] = value;

        if (cablesStatus.Zip(cablesStatus, (first, second) => first && second).All(isConnected => isConnected))
        {
            AllCablesArrived();
        }
    }

    private void AllCablesArrived()
    {
        cableButton.enabled = true;
        winBell.Play();
        allCablesArrived = true;
        explosionSound.Play();
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

        clockAnim.speed = 0;
    }
    #endregion PaintPuzzle

    #endregion Puzzles

    public IEnumerator JumpscareLookAt(Transform jumpscare)
    {
        canMove = false;
        yield return new WaitForSeconds(0.5f);

        float rotationSpeed = 1f;
        Quaternion targetRotation = Quaternion.LookRotation(jumpscare.position - cameraTransform.position);

        while (Quaternion.Angle(cameraTransform.rotation, targetRotation) > 0.1f)
        {
            cameraTransform.rotation = Quaternion.RotateTowards(cameraTransform.rotation, targetRotation, rotationSpeed);
            yield return null;
        }

        //Abrir puertas
        if (LaserBeam.Instance.playerOnUpside)
        {
            firstPuzzleUpsideLeftDoor.SetBool("IsTrue", true);
            firstPuzzleUpsideRightDoor.SetBool("IsTrue", true);
        }
        else
        {
            firstPuzzleRealLeftDoor.SetBool("IsTrue", true);
            firstPuzzleRealRightDoor.SetBool("IsTrue", true);
        }

        //Sonido de abrir puertas
        doorOpenSound.Play();
        yield return new WaitForSeconds(0.5f);


        //Shake de camera
        cameraShake.TriggerShake(4f);
        //Efecto estática de camera
        //Bicho hacia el jugador, solo el que comparta mundo, el otro dejarlo quieto
        if (LaserBeam.Instance.playerOnUpside)
        {
            upsideScreamer.TeleportToPlayer();
        }
        else
        {
            realScreamer.TeleportToPlayer();
        }
        //Sonido fuerte
        jumpscareSound.Play();

        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("LostScene");
    }

    public void DecreaseLevelTime()
    {
        firstBeats.Play();

        if (Timer.Instance.last30)
        {
            firstBeats.Stop();
            last30Secs.Play();
            mirror.Play();

            foreach (var item in pumpkins)
            {
                item.SetActive(true);
                //Sonido de aviso de miedo
                //Algun mini shake de camera
                //Algun efecto en la cámara como de estática
            }
        }
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