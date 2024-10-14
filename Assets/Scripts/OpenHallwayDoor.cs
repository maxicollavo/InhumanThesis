using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenHallwayDoor : MonoBehaviour, Interactor
{
    [SerializeField] Animator doorAnim;
    [SerializeField] Animator otherDoorAnim;

    public bool isEnd;

    public TextMeshProUGUI counterText;

    void Update()
    {
        counterText.text = Mathf.FloorToInt(GameManager.Instance.levelCounter).ToString();
    }

    public void Interact()
    {
        doorAnim.SetBool("IsTrue", true);
        otherDoorAnim.SetBool("IsTrue", true);

        if (isEnd)
        {
            SceneManager.LoadScene("EndDemoScene");
        }
    }
}
