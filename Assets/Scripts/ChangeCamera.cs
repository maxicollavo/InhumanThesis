using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour, Interactor
{
    [SerializeField] List<GameObject> mainCams;
    [SerializeField] GameObject changeCam;

    [SerializeField] GameObject bodyLight;

    public void Interact()
    {
        if (mainCams[0].activeInHierarchy)
        {
            GameManager.Instance.canMove = false;

            foreach (var cam in mainCams)
            {
                cam.SetActive(false);
            }

            changeCam.SetActive(true);
            bodyLight.SetActive(true);

        }
        else
        {
            GameManager.Instance.canMove = true;

            foreach (var cam in mainCams)
            {
                cam.SetActive(true);
            }

            changeCam.SetActive(false);
            bodyLight.SetActive(false);
        }
    }
}
