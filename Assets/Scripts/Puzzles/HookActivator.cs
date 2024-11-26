using System.Collections.Generic;
using UnityEngine;

public class HookActivator : MonoBehaviour, Interactor
{
    public List<HookBehaviour> hooks = new List<HookBehaviour>();
    [SerializeField] AudioSource audio;

    public void Interact()
    {
        foreach (var hook in hooks)
        {
            hook.Mover();
            hook.WinChecker();
        }
        audio.Play();
    }
}
