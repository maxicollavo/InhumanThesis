using System.Collections;
using TMPro;
using UnityEngine;

public class Jeroglific : MonoBehaviour, IRead
{
    public TextMeshProUGUI subtitle;
    public string text;

    public void Read()
    {
        StartCoroutine(SetSubtitle());
    }

    IEnumerator SetSubtitle()
    {
        subtitle.gameObject.SetActive(true);
        subtitle.text = text;
        yield return new WaitForSeconds(3f);
        subtitle.gameObject.SetActive(false);
    }
}
