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
        TurnObjects();
    }

    private IEnumerator TurnObjects()
    {
        GameObject activeObj = presentObj.activeSelf ? presentObj : pastObj;
        GameObject inactiveObj = presentObj.activeSelf ? pastObj : presentObj;

        yield return new WaitForSeconds(1f);

        //StartCoroutine(activeObj.DissolveCo());

        yield return new WaitForSeconds(0.3f);

        inactiveObj.SetActive(true);
        //StartCoroutine(inactiveObj.RestoreCo());
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
