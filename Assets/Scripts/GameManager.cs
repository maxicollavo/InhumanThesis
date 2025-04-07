using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Input Manager")]
    [HideInInspector]
    public bool canMove = true;

    [Header("Power Manager")]
    public bool ableToTeleport;

    [Header("States Manager")]
    [HideInInspector]
    public PowerStates state;

    [Header("Pause Manager")]
    [SerializeField] GameObject pauseMenu;
    private bool menuPressed;
    [HideInInspector]

    [Header("Gameplay")]
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
        EventManager.Instance.Register(GameEventTypes.OnCinematic, OnCinematicMethod);
        EventManager.Instance.Register(GameEventTypes.OnGameplay, OnGameplayMethod);
        EventManager.Instance.Register(GameEventTypes.OnPuzzle, OnPuzzleMethod);
    }

    private void OnDestroy()
    {
        EventManager.Instance.Unregister(GameEventTypes.OnCinematic, OnCinematicMethod);
        EventManager.Instance.Unregister(GameEventTypes.OnGameplay, OnGameplayMethod);
        EventManager.Instance.Unregister(GameEventTypes.OnPuzzle, OnPuzzleMethod);
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

    public void OnCinematicMethod(object sender, EventArgs e)
    {
        canMove = false;
    }

    public void OnPuzzleMethod(object sender, EventArgs e)
    {
        canMove = false;
    }

    public void OnGameplayMethod(object sender, EventArgs e)
    {
        canMove = true;
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