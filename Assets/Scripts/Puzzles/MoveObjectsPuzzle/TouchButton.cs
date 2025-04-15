using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchButton : MonoBehaviour
{
    private TouchFigure figureToMove;
    private Waypoints actualWp;
    private float moveSpeed = 5f;
    Outline outline;

    [SerializeField] Vector3 direction;

    private void Awake()
    {
        outline = GetComponent<Outline>();
    }

    private void Start()
    {
        outline.enabled = false;
    }

    private void OnMouseDown()
    {
        StartCoroutine(GetFigure());
    }

    private IEnumerator GetFigure()
    {
        outline.enabled = true;
        MovePuzzleManager.instance.SetButtonsEnabled(false);
        figureToMove = MovePuzzleManager.instance.selectedFigure;
        actualWp = MovePuzzleManager.instance.wp;

        yield return new WaitForEndOfFrame();

        MoveFigure();
    }

    private void MoveFigure()
    {
        Transform target = null;
        Waypoints nextWaypoint = null;

        if (direction == Vector3.right && actualWp.waypointRight != null && !actualWp.waypointRight.IsUsing)
        {
            target = actualWp.waypointRight.transform;
            nextWaypoint = actualWp.waypointRight;
            actualWp.waypointRight.IsUsing = true;
        }
        else if (direction == Vector3.left && actualWp.waypointLeft != null && !actualWp.waypointLeft.IsUsing)
        {
            target = actualWp.waypointLeft.transform;
            nextWaypoint = actualWp.waypointLeft;
            actualWp.waypointLeft.IsUsing = true;
        }
        else if (direction == Vector3.up && actualWp.waypointUp != null && !actualWp.waypointUp.IsUsing)
        {
            target = actualWp.waypointUp.transform;
            nextWaypoint = actualWp.waypointUp;
            actualWp.waypointUp.IsUsing = true;
        }
        else if (direction == Vector3.down && actualWp.waypointDown != null && !actualWp.waypointDown.IsUsing)
        {
            target = actualWp.waypointDown.transform;
            nextWaypoint = actualWp.waypointDown;
            actualWp.waypointDown.IsUsing = true;
        }

        if (target != null)
            StartCoroutine(MoveToTarget(figureToMove.transform, target.position, nextWaypoint, actualWp));
        else
            StartCoroutine(CannotMove());
    }

    private IEnumerator MoveToTarget(Transform obj, Vector3 targetPos, Waypoints newWp, Waypoints oldWp)
    {
        while (Vector3.Distance(obj.position, targetPos) > 0.01f)
        {
            obj.position = Vector3.MoveTowards(obj.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        obj.position = targetPos;
        yield return new WaitForSeconds(0.2f);

        oldWp.IsUsing = false;
        newWp.IsUsing = true;

        MovePuzzleManager.instance.wp = newWp;

        figureToMove.OnTarget = newWp.IsTarget;

        outline.enabled = false;
        MovePuzzleManager.instance.SetButtonsEnabled(true);
        MovePuzzleManager.instance.CheckPosition(newWp);
    }

    private IEnumerator CannotMove()
    {
        outline.OutlineColor = Color.red;
        yield return new WaitForSeconds(0.5f);
        outline.OutlineColor = Color.white;
        outline.enabled = false;
        MovePuzzleManager.instance.SetButtonsEnabled(true);
    }
}