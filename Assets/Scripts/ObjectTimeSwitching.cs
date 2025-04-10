using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTimeSwitching : MonoBehaviour, ISwitcheable
{
    [SerializeField] GameObject pastObj;
    [SerializeField] GameObject presentObj;

    [SerializeField] List<Outline> outlines;

    private void Start()
    {
        DisableOutline();
    }

    public void Aiming()
    {
        EnableOutline();
    }

    public void Switch()
    {
       StartCoroutine(TurnObjects());
    }

    private IEnumerator TurnObjects()
    {
        GameObject activeObj = presentObj.activeSelf ? presentObj : pastObj;
        GameObject inactiveObj = presentObj.activeSelf ? pastObj : presentObj;

       DisolveController activeDC = activeObj.GetComponent<DisolveController>();
       DisolveController inactiveDC = inactiveObj.GetComponent<DisolveController>();

       StartCoroutine(activeDC.DissolveCo());
        yield return new WaitForSeconds(1f);


        yield return new WaitForSeconds(0.3f);
        StartCoroutine(inactiveDC.DissolveCo());
        inactiveObj.SetActive(true);
        Debug.Log("inactiveObj activado: " + inactiveObj.name);
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
