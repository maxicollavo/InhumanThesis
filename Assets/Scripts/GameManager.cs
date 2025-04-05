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

    public bool ableToTeleport;

    private Transform cameraTransform;


    [HideInInspector]
    public PowerStates state;

    [SerializeField] GameObject pauseMenu;
    private bool menuPressed;
    [HideInInspector]

    public List<GameObject> TPWaypoints;

    //public PostProcessProfile profile;
    //private UnityEngine.Rendering.PostProcessing.ChromaticAberration ca;
    //private UnityEngine.Rendering.PostProcessing.ColorGrading cg;

    public CameraShake cameraShake;

    public bool isAimingAtObject;

    public static GameManager Instance { get; set; }

    private void Awake()
    {
        Instance = this;

        isAimingAtObject = false;
    }

    private void Start()
    {
        cameraTransform = Camera.main.transform;

        //ca = profile.GetSetting<UnityEngine.Rendering.PostProcessing.ChromaticAberration>();
        //cg = profile.GetSetting<UnityEngine.Rendering.PostProcessing.ColorGrading>();
        //ca.intensity.Override(0);
        //cg.active = false;
    }

    public void Update()
    {
        if (!canMove)
        {
            Debug.Log("Se detiene el GameManager");
            return;
        }

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
}

public enum PowerStates
{
    OnLaser,
    OnDimension
}

public enum RailColors
{
    Red,
    Blue,
    Yellow,
    Green
}