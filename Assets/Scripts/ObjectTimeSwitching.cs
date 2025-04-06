using System.Collections.Generic;
using UnityEngine;

public class ObjectTimeSwitching : MonoBehaviour, ISwitcheable
{
    [SerializeField] List<GameObject> T_Objects;

    [SerializeField] List<Outline> outlines;

    private void Start()
    {
        DisableOutline();
    }

    //Metodo para cuando solo apuntamos al objeto
    public void Aiming()
    {
        Debug.Log("Is Aiming");
        EnableOutline();
    }

    public void Switch()
    {
        Debug.Log("Switch Object");
        TurnObjects();
    }

    private void TurnObjects()
    {
        //Activar shader desaparecer del objeto futuro
        foreach (var obj in T_Objects)
        {
            obj.SetActive(!obj.activeSelf);
        }
        //Activar shader aparecer del objeto pasado
    }

    void EnableOutline()
    {
        foreach(var o in outlines)
        {
            o.enabled = true;
        }
    }

    public void DisableOutline()
    {
        foreach (var o in outlines)
        {
            o.enabled = false;
        }
    }
}
