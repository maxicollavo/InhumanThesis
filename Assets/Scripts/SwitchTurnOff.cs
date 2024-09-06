using UnityEngine;
using System;
using System.Collections;

public class SwitchTurnOff : MonoBehaviour, Interactor
{
    [SerializeField] GameObject objectToTurnOff;
    public Animation Anim1;
    public Animation Anim2;
    public void Interact()
    {
       //OpenDoor();
         objectToTurnOff.SetActive(false);
    }

    /*public  IEnumerator OpenDoor() 
    {
       Anim1.Play();
       Anim2.Play();
       yield return new WaitForSeconds(1);
       objectToTurnOff.SetActive(false);
    }
     */   
}


