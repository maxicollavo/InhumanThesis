using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenHallwayDoor : MonoBehaviour
{
    [SerializeField] Animator doorAnim;
    [SerializeField] Animator otherDoorAnim;

    public bool isEnd;

    private Renderer objectRenderer;
    private Material thisMat;
    [SerializeField] Material newMat;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        thisMat = objectRenderer.material;
    }

    public void Interact()
    {
        doorAnim.SetBool("IsTrue", true);
        otherDoorAnim.SetBool("IsTrue", true);

        objectRenderer.material = newMat;

        if (isEnd)
        {
            SceneManager.LoadScene("EndDemoScene");
        }
    }
}
