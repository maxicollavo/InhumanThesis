using UnityEngine;

public class LightPrueba : MonoBehaviour
{
    public Light lightSource;  // La luz que queremos que titile
    private int secondsRemaining = 5;  // Tiempo inicial en segundos
    private float timer = 0f;

    void Start()
    {
        if (lightSource == null)
        {
            lightSource = GetComponent<Light>();
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 1f && secondsRemaining > 0)
        {
            timer = 0f;
            StartCoroutine(BlinkLight(secondsRemaining));
            secondsRemaining--;
        }
    }

    System.Collections.IEnumerator BlinkLight(int blinkCount)
    {
        for (int i = 0; i < blinkCount; i++)
        {
            lightSource.enabled = true;
            yield return new WaitForSeconds(0.1f);  // Duración del destello
            lightSource.enabled = false;
            yield return new WaitForSeconds(0.1f);  // Intervalo entre destellos
        }
    }
}
