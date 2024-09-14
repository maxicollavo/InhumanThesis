using UnityEngine;
using System;
using System.Collections;

public class SwitchTurnOff : MonoBehaviour, Interactor
{
    [SerializeField] GameObject objectToTurnOff;
    public Animator Anim3;

    private void Start()
    {
        Anim3 = gameObject.GetComponent<Animator>();
    }
    public void Interact()
    {
        //OpenDoor();
        objectToTurnOff.SetActive(false);
          Anim3.SetBool("Istrue", true);
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


