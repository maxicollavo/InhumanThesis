using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AnimacionesPyramid : MonoBehaviour
{
    [SerializeField] ParticleSystem chispas;
    [SerializeField] GameObject top;

    [SerializeField] Animator anim;

    private void Start()
    {
        EventManager.Instance.Register(GameEventTypes.OnGameplay, RestartAnim);
    }

    private void OnDestroy()
    {
        EventManager.Instance.Register(GameEventTypes.OnGameplay, RestartAnim);
    }

    void Close()
    {
        Instantiate(chispas, top.transform);
    }

    void RestartAnim(object sender, EventArgs e)
    {
        Debug.Log("Reinicia anim piramide");
        anim.Play("PyramidStartLevitation", -1, 0f);
    }
}
