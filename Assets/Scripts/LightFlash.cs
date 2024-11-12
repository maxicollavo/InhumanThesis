using System.Collections;
using UnityEngine;

public class LightFlash : MonoBehaviour
{
    public float timeDelay;
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
            timeDelay = Random.Range(0.03f, 0.5f);
            yield return new WaitForSeconds(timeDelay);
            floorSpotlight.enabled = true;
            sealingSpotlight.enabled = true;
            timeDelay = Random.Range(0.03f, 0.5f);
            yield return new WaitForSeconds(timeDelay);
        }
    }
}