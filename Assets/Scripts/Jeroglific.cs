using System.Collections;
using TMPro;
using UnityEngine;

public class Jeroglific : MonoBehaviour, IRead
{
    public TextMeshProUGUI subtitle;
    public string text;

    [SerializeField] Outline outline;

    private void Awake()
    {
        outline = GetComponent<Outline>();
    }

    private void Start()
    {
        DisableOutline();
    }

    public void Aiming()
    {
        EnableOutline();

        UIManager.Instance.ChangeCursor(true);
    }

    public void Read()
    {
        StartCoroutine(SetSubtitle());
    }

    IEnumerator SetSubtitle()
    {
        GameManager.Instance.clickBlock = true;

        subtitle.gameObject.SetActive(true);
        subtitle.text = text;

        yield return new WaitForSeconds(3f);

        GameManager.Instance.clickBlock = false;

        subtitle.gameObject.SetActive(false);
    }

    void EnableOutline()
    {
        outline.enabled = true;
    }

    public void DisableOutline()
    {
        outline.enabled = false;

        UIManager.Instance.ChangeCursor(false);
    }
}