using System;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public List<BoxCollider> puzzleBoxColliders;
    public List<BoxCollider> inPuzzleColliders;

    private void Start()
    {
        EventManager.Instance.Register(GameEventTypes.OnGameplay, OnGameplayMethod);
        EventManager.Instance.Register(GameEventTypes.OnPuzzle, OnPuzzleMethod);
    }

    private void OnDestroy()
    {
        EventManager.Instance.Unregister(GameEventTypes.OnGameplay, OnGameplayMethod);
        EventManager.Instance.Unregister(GameEventTypes.OnPuzzle, OnPuzzleMethod);
    }

    private void OnPuzzleMethod(object sender, EventArgs e)
    {
        foreach (var coll in puzzleBoxColliders)
        {
            coll.enabled = false;
        }

        foreach (var puz in inPuzzleColliders)
        {
            puz.enabled = true;
        }
    }

    private void OnGameplayMethod(object sender, EventArgs e)
    {
        foreach (var coll in puzzleBoxColliders)
        {
            coll.enabled = true;
        }

        foreach (var puz in inPuzzleColliders)
        {
            puz.enabled = false;
        }
    }
}
