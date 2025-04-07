using UnityEngine;

public class ColumnAnimPass : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject newWall;

    [SerializeField] private bool IsLastColumn;

    public void ActivateAnim()
    {
        if (IsLastColumn)
        {
            wall.SetActive(false);
            newWall.SetActive(true);
        }
        else
        {
            anim.SetTrigger("Interact");
        }
    }
}
