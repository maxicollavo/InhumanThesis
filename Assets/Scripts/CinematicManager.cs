using System;
using System.Collections;
using UnityEngine;

public class CinematicManager : MonoBehaviour
{
    [SerializeField] private AnimacionesPyramid pyramid;

    public void Back()
    {
        StartCoroutine(BackToGameplay());
    }

    IEnumerator BackToGameplay()
    {
        EventManager.Instance.Dispatch(GameEventTypes.OnGameplay, this, EventArgs.Empty);
        yield return new WaitForSeconds(0.1f);
        pyramid.RestartAnim();
    }
}