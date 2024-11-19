using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailPuzzle : MonoBehaviour
{
    public List<Transform> posInRail = new List<Transform>();
    public List<GameObject> thisButtons = new List<GameObject>();

    public int posCounter;

    [HideInInspector] public bool state;

    [SerializeField] int winPos;
    [SerializeField] int railNum;

    private Transform childTransform;

    [SerializeField] AudioSource statueSound;

    private void Awake()
    {
        childTransform = transform.GetChild(0);
        GameManager.Instance.rail.Add(state);
    }

    public void GoForward()
    {
        if (posCounter == 4)
        {
            //Sonido de NO SE PUEDE
            return;
        }

        statueSound.Play();
        posCounter++;

        var newPos = posInRail[posCounter].position;

        StartCoroutine(MoveChildTransform(newPos));

        CheckPos();
    }

    public void GoBackwards()
    {
        if (posCounter == 0) return;

        statueSound.Play();
        posCounter--;

        var newPos = posInRail[posCounter].position;

        StartCoroutine(MoveChildTransform(newPos));

        CheckPos();
    }

    private void CheckPos()
    {
        if (posCounter == winPos)
        {
            GameManager.Instance.rail[railNum] = true;
        }
        else
        {
            GameManager.Instance.rail[railNum] = false;
        }

        RailManager.Instance.CheckWin();
    }

    private IEnumerator MoveChildTransform(Vector3 targetPosition)
    {
        foreach (var button in thisButtons)
        {
            var coll = button.GetComponent<BoxCollider>();
            coll.enabled = false;
        }

        float time = 0;
        Vector3 startPosition = childTransform.position;

        while (time < 1)
        {
            time += Time.deltaTime;
            childTransform.position = Vector3.Lerp(startPosition, targetPosition, time);
            yield return null;
        }

        childTransform.position = targetPosition;
        yield return new WaitForSeconds(0.25f);

        foreach (var button in thisButtons)
        {
            var coll = button.GetComponent<BoxCollider>();
            coll.enabled = true;
        }
    }
}
