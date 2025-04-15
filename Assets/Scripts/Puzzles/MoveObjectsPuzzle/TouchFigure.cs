using UnityEngine;

public class TouchFigure : MonoBehaviour
{
    [SerializeField] Waypoints wp;
    Outline outline;
    public bool IsSelected;

    public bool OnTarget;
    public int index;
    public bool OnPositionWinner;

    private void Awake()
    {
        transform.position = wp.transform.position;
        outline = GetComponent<Outline>();
    }

    private void Start()
    {
        outline.enabled = false;
    }

    private void OnMouseDown()
    {
        if (MovePuzzleManager.instance.selectedFigure != null && MovePuzzleManager.instance.selectedFigure != this)
        {
            MovePuzzleManager.instance.selectedFigure.DeselectFigure();
        }

        MovePuzzleManager.instance.GetFigure(this, wp);
        IsSelected = true;
        outline.enabled = true;
        MovePuzzleManager.instance.SetButtonsEnabled(true);
    }

    private void OnMouseEnter()
    {
        if (IsSelected) return;

        outline.enabled = true;
    }

    private void OnMouseExit()
    {
        if (IsSelected) return;

        outline.enabled = false;
    }

    public void DeselectFigure()
    {
        IsSelected = false;
        outline.enabled = false;
    }
}