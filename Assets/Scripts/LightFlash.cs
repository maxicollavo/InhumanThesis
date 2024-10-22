using System.Collections;
using UnityEngine;

public class LightFlash : MonoBehaviour
{
    private float timeDelay;
    public Light floorSpotlight;
    public Light sealingSpotlight;

    private bool isActivated;

    private void Start()
    {
        isActivated = true;

        StartCoroutine(TorchLight());
    }

    IEnumerator TorchLight()
    {
        while (isActivated)
        {
            floorSpotlight.enabled = false;
            sealingSpotlight.enabled = false;
            timeDelay = Random.Range(0.01f, 0.2f);
            yield return new WaitForSeconds(timeDelay);
            floorSpotlight.enabled = true;
            sealingSpotlight.enabled = true;
            timeDelay = Random.Range(0.01f, 0.2f);
            yield return new WaitForSeconds(timeDelay);
        }
    }
}