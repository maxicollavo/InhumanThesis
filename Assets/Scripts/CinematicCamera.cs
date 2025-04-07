using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CinematicCamera : MonoBehaviour
{
    [SerializeField] private GameObject _playerCam;
    [SerializeField] private float rotationSpeed = 2f;

    [Header("Cinematic Targets")]
    [SerializeField] private Transform[] lookTargets;

    private bool rotatingToTarget = false;
    private Transform targetToLookAt;

    private void Start()
    {
        EventManager.Instance.Register(GameEventTypes.OnCinematic, Cinematic);

        gameObject.SetActive(false);
    }


    private void OnDestroy()
    {
        EventManager.Instance.Unregister(GameEventTypes.OnCinematic, Cinematic);
    }

    private void Update()
    {
        if (!gameObject.activeSelf) return;

        if (rotatingToTarget && targetToLookAt != null)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetToLookAt.position - transform.position);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
            {
                rotatingToTarget = false;
            }
        }
    }

    void Cinematic(object sender, EventArgs e)
    {
        Debug.Log("enciende la camara");    

        transform.position = _playerCam.transform.position;
        transform.rotation = _playerCam.transform.rotation;

        _playerCam.SetActive(false);
        gameObject.SetActive(true);
    }

    public void RotateCamera(int targetIndex)
    {
        if (targetIndex >= 0 && targetIndex < lookTargets.Length)
        {
            targetToLookAt = lookTargets[targetIndex];
            rotatingToTarget = true;
        }
    }

    public void ResetCamera()
    {
        _playerCam.SetActive(true);

        _playerCam.transform.position = transform.position;
        _playerCam.transform.rotation = transform.rotation;

        gameObject.SetActive(false);

        EventManager.Instance.Dispatch(GameEventTypes.OnGameplay, this, EventArgs.Empty);
    }

    IEnumerator ResetAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ResetCamera();
    }
}
