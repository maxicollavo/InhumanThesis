using System.Collections;
using TMPro;
using UnityEngine;

public class TutorialTriggers : MonoBehaviour, ITutorial
{
    [SerializeField] GameObject subtitle;
    [SerializeField] GameObject crouchTrigger;
    [SerializeField] GameObject door;
    [SerializeField] string subText;

    TextMeshProUGUI ui;

    [SerializeField] bool DestroysTrigger;
    [SerializeField] bool StartsLevel;

    private void Awake()
    {
        ui = subtitle.GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        ui.text = subText;
    }

    public void Exit()
    {
        subtitle.SetActive(false);
    }

    public void Interact()
    {
        if (DestroysTrigger)
        {
            Destroy(crouchTrigger);
            return;
        }

        if (StartsLevel)
        {
            door.SetActive(true);
            Destroy(gameObject);
            return;
        }

        if (subtitle != null)
            subtitle.SetActive(true);
    }
}
