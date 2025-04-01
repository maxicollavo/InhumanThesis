using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTimeSwitching : MonoBehaviour
{
    [SerializeField] GameObject futObj;
    [SerializeField] GameObject pastObj;

    private bool isAiming;

    private void Awake()
    {
        futObj.SetActive(true);
        pastObj.SetActive(false);

        isAiming = false;
    }

    //Metodo para cuando solo apuntamos al objeto
    private void Aiming()
    {

    }

    //Metodo para cuando queremos cambiar el objeto
    private void Interact()
    {

    }

    private void TurnObjects()
    {
        //Activar shader desaparecer del objeto futuro
        futObj.SetActive(false);
        pastObj.SetActive(true);
        //Activar shader aparecer del objeto pasado
    }

    private void ToggleOutline()
    {
        if (futObj.activeInHierarchy)
        {
            //Activar OUTLINE del objeto futuro
        }
        else
        {
            //Activar OUTLINE del objeto pasado
        }
    }
}
