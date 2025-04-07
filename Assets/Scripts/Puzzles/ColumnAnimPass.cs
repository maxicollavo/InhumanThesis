using UnityEngine;

public class ColumnAnimPass : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject newWall;
    [SerializeField] private GameObject interactTrigger;

    [SerializeField] private bool IsLastColumn;
    [SerializeField] CinematicCamera cinematicCamera;

    public void ActivateAnim(int target)
    {
        if (IsLastColumn)
        {
            wall.SetActive(false);
            newWall.SetActive(true);
            interactTrigger.SetActive(true);

            cinematicCamera.ResetCamera();
        }
        else
        {
            anim.SetTrigger("Interact");
            cinematicCamera.RotateCamera(target);
        }
    }
}
