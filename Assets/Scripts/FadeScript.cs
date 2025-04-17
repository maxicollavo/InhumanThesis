using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class FadeScript : MonoBehaviour
{
    public List<CanvasGroup> powerCanvases; // Lista de UI para cada poder
    public float fadeSpeed = 1f;
    public float displayTime = 1f;

    private Coroutine currentRoutine;

    public void ShowUI(int powerIndex)
    {
        if (currentRoutine != null)
        {
            StopCoroutine(currentRoutine);
        }

        currentRoutine = StartCoroutine(FadeSequence(powerIndex));
    }

    private IEnumerator FadeSequence(int index)
    {
        // Aseguramos que solo uno está activo
        for (int i = 0; i < powerCanvases.Count; i++)
        {
            if (i != index)
            {
                powerCanvases[i].alpha = 0;
                powerCanvases[i].gameObject.SetActive(false);
            }
        }

        CanvasGroup selected = powerCanvases[index];
        selected.gameObject.SetActive(true);

        // Fade In
        while (selected.alpha < 1)
        {
            selected.alpha += Time.deltaTime * fadeSpeed;
            yield return null;
        }
        selected.alpha = 1;
        Debug.Log("Activando: " + powerCanvases[index].name);
        // Espera visible
        yield return new WaitForSeconds(displayTime);

        // Fade Out
        while (selected.alpha > 0)
        {
            selected.alpha -= Time.deltaTime * fadeSpeed;
            yield return null;
        }
        selected.alpha = 0;
        selected.gameObject.SetActive(false);
    }
}
