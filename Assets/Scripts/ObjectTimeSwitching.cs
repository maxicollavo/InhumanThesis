using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTimeSwitching : MonoBehaviour, ISwitcheable
{
    [SerializeField] GameObject futObj;
    [SerializeField] GameObject pastObj;

    public bool isAiming;
    //Hacer que cuando deje de apuntar isAiming pase a false

    private void Awake()
    {
        futObj.SetActive(true);
        pastObj.SetActive(false);

        isAiming = false;
    }

    //Metodo para cuando solo apuntamos al objeto
    public void Aiming()
    {
        ToggleOutline();
        Debug.Log("Is Aiming");
    }

    private void TurnObjects()
    {
        //Activar shader desaparecer del objeto futuro
        futObj.SetActive(!futObj.activeSelf);
        pastObj.SetActive(!pastObj.activeSelf);
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

    public void Switch()
    {
        TurnObjects();
    }
}
