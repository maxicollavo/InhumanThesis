using System.Collections.Generic;
using UnityEngine;

public class ObjectTimeSwitching : MonoBehaviour, ISwitcheable
{
    [SerializeField] List<GameObject> T_Objects;


    private bool hasMat;
    private Material originalMat;
    private Material tempMat;
    [SerializeField] Material newMat;

    private void Update()
    {
        //if (!GameManager.Instance.isAimingAtObject && hasMat)
        //{
        //    RestoreOriginalMaterial();
        //    Debug.Log("Devuelve el material original");
        //    hasMat = false;
        //}
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
        foreach (var obj in T_Objects)
        {
            obj.SetActive(!obj.activeSelf);
        }
        //Activar shader aparecer del objeto pasado
    }

    private void ToggleOutline()
    {
        GameManager.Instance.isAimingAtObject = true;

        if (hasMat) return;

        foreach (var obj in T_Objects)
        {
            if (obj.activeSelf)
            {
                Debug.Log("Cambia el material del objeto");
                var renderer = obj.GetComponent<Renderer>();
                originalMat = renderer.material;
                renderer.material = newMat;

                hasMat = true;
            }
        }
    }

    private void RestoreOriginalMaterial()
    {
        foreach (var obj in T_Objects)
        {
            if (obj.activeSelf)
            {
                var renderer = obj.GetComponent<Renderer>();
                renderer.material = originalMat;
            }
        }
    }

    public void Switch()
    {
        TurnObjects();
    }
}
