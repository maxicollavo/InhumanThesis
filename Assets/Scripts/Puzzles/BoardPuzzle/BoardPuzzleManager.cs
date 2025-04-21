using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class BoardPuzzleManager : MonoBehaviour
{
    //Codigo grueso, va a escuchar los action de los actores y realizar las acciones necesarias
    [Header("Piece Movement")]
    public float moveSpeed;
    bool isMoving;

    [Header("Pieces")]
    public BoardPiece[] pieces;
    private BoardPiece selectedPiece;
    public GameObject pieceGo;
    public GameObject handPiece;

    [Header("Waypoints")]
    private BoardWaypoint currentWp;
    private BoardWaypoint newWaypoint;
    [SerializeField] private BoardWaypoint[] targets;

    [Header("Buttons")]
    public BoardButton[] buttons;
    private BoardButton pressedButton;
    private Vector2 direction;

    [Header("Colliders")]
    [SerializeField] List<BoxCollider> colliders;
    private bool previousState;

    [Header("Settings")]
    [SerializeField] BoxCollider interactorCollider;
    public bool OnPuzzle { get; private set; }
    [SerializeField] Camera puzzleCam;
    private bool HasPiece;
    private bool HasWon;

    [Header("On Win")]
    [SerializeField] List<Animator> doors;

    private Dictionary<BoardPiece, BoardWaypoint> pieceTargetMap = new Dictionary<BoardPiece, BoardWaypoint>();

    [SerializeField] PuzzleInteractor interactor;

    private void Start()
    {
        puzzleCam.enabled = false;

        foreach (var p in pieces)
        {
            p.OnPieceSelected += GetSelectedPiece;
        }

        foreach (var b in buttons)
        {
            b.OnButtonPressed += GetButtonPressed;
        }

        for (int i = 0; i < pieces.Length; i++)
        {
            pieceTargetMap[pieces[i]] = targets[i];
        }

        foreach (var c in colliders)
        {
            c.enabled = false;
        }

        interactor.PuzzleAction += OnPuzzleMethod;
    }

    private void Update()
    {
        bool currentState = OnPuzzle;

        if (currentState != previousState)
        {
            SwitchColliders(currentState);
            previousState = currentState;
        }

        if (currentState && Input.GetKeyDown(KeyCode.Mouse1))
        {
            BackToGameplay();
        }
    }

    void OnPuzzleMethod(PuzzleInteractor interactor)
    {
        if (HasWon) return;

        if (GameManager.Instance.HasPiece)
        {
            HasPiece = true;
            pieceGo.SetActive(true);
            handPiece.SetActive(false);
            GameManager.Instance.HasPiece = false;
            return;
        }

        if (!HasPiece) return;

        interactor.DisableOutline();
        puzzleCam.enabled = true;
        interactorCollider.enabled = false;
        OnPuzzle = true;
        EventManager.Instance.Dispatch(GameEventTypes.OnPuzzle, this, EventArgs.Empty);
    }

    public void BackToGameplay()
    {
        puzzleCam.enabled = false;
        OnPuzzle = false;
        EventManager.Instance.Dispatch(GameEventTypes.OnGameplay, this, EventArgs.Empty);
        if (HasWon) return;

        interactorCollider.enabled = true;
    }

    void SwitchColliders(bool state)
    {
        Debug.Log("Cambia el estado");
        foreach (var c in colliders)
        {
            c.enabled = state;
        }
    }

    void GetButtonPressed(Vector2 pressedDirection, BoardButton button)
    {
        if (isMoving) return;

        isMoving = true;
        direction = pressedDirection;

        if (selectedPiece == null && currentWp == null) return;

        if (currentWp.neighbors.TryGetValue(direction, out BoardWaypoint nextWp) && !nextWp.IsUsing)
        {
            newWaypoint = nextWp;

            StartCoroutine(MoveToTarget(selectedPiece, newWaypoint.transform.position, currentWp, nextWp, button));
        }
        else
        {
            StartCoroutine(CannotMove(button));
        }
    }

    void GetSelectedPiece(BoardPiece piece)
    {
        if (selectedPiece != null)
        {
            selectedPiece.DeselectPiece();
        }

        selectedPiece = piece;
        currentWp = piece.currentWp;
    }

    private IEnumerator MoveToTarget(BoardPiece piece, Vector3 targetPos, BoardWaypoint currentW, BoardWaypoint nextW, BoardButton button)
    {
        button.EnableOutline();

        while (Vector3.Distance(piece.transform.position, targetPos) > 0.01f)
        {
            piece.transform.position = Vector3.MoveTowards(piece.transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        piece.transform.position = targetPos;
        yield return new WaitForSeconds(0.2f);

        currentW.IsUsing = false;
        nextW.IsUsing = true;

        piece.currentWp = nextW;
        currentWp = nextW;
        isMoving = false;
        button.DisableOutline();

        CheckPosition(piece, nextW);
    }

    private IEnumerator CannotMove(BoardButton button)
    {
        button.EnableOutline();
        button.outline.OutlineColor = Color.red;
        yield return new WaitForSeconds(0.5f);
        button.outline.OutlineColor = Color.white;
        button.DisableOutline();
        isMoving = false;
    }

    private void CheckPosition(BoardPiece piece, BoardWaypoint wp)
    {
        if (!pieceTargetMap.ContainsKey(piece)) return;

        var targetWp = pieceTargetMap[piece];

        if (wp == targetWp)
        {
            piece.OnPositionWinner = true;
        }
        else
        {
            piece.OnPositionWinner = false;
        }

        CheckWin();
    }

    private void CheckWin()
    {
        foreach (var piece in pieces)
        {
            if (!piece.OnPositionWinner) return;
        }

        Win();
    }

    void Win()
    {
        HasWon = true;
        foreach (var door in doors) door.SetTrigger("Open");
        foreach (var piece in pieces) piece.DisableOutline();
        BackToGameplay();
    }

    //void Win()
    //{
    //    foreach (var coll in allColliders)
    //    {
    //        Destroy(coll);
    //    }

    //    cinematicCam.gameObject.SetActive(true);
    //    cinematicCam.transform.position = playerCam.transform.position;
    //    cinematicCam.transform.rotation = playerCam.transform.rotation;

    //    EventManager.Instance.Dispatch(GameEventTypes.OnCinematic, this, EventArgs.Empty);

    //    winTimeline.Play();
    //    StartCoroutine(WaitForTimeline());
    //}

    //IEnumerator WaitForTimeline()
    //{
    //    yield return new WaitUntil(() => winTimeline.state != PlayState.Playing);
    //    EventManager.Instance.Dispatch(GameEventTypes.OnGameplay, this, EventArgs.Empty);
    //}
}