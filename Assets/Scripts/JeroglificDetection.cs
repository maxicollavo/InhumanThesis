using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JeroglificDetection : MonoBehaviour
{
    public float detectionRange = 10f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            DetectJeroglific();
        }
    }

    void DetectJeroglific()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, detectionRange))
        {
            var obj = hit.collider.GetComponent<Jeroglific>();
            if (obj != null)
            {
                obj.Read();
            }
        }
    }
}
