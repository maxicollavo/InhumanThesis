using UnityEngine;

public class PuzzleCursorMove : MonoBehaviour
{
    private void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
