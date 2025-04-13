using System;
using System.Collections;
using UnityEngine;

public class CinematicCamera : MonoBehaviour
{
    [SerializeField] private Transform _playerCam;
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

            if (_playerCam != null)
            {
                Quaternion camTargetRot = Quaternion.LookRotation(targetToLookAt.position - _playerCam.position);
                _playerCam.rotation = Quaternion.Lerp(_playerCam.rotation, camTargetRot, Time.deltaTime * rotationSpeed);
            }

            if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
            {
                rotatingToTarget = false;
            }
        }
    }

    void Cinematic(object sender, EventArgs e)
    {
        transform.position = _playerCam.transform.position;
        transform.rotation = _playerCam.transform.rotation;

        _playerCam.gameObject.SetActive(false);
        gameObject.SetActive(true);
    }

    public void RotateCamera(int targetIndex)
    {
        Debug.Log("Rota la camara");
        if (targetIndex >= 0 && targetIndex < lookTargets.Length)
        {
            targetToLookAt = lookTargets[targetIndex];
            rotatingToTarget = true;
        }
    }

    public void ResetCamera()
    {
        _playerCam.gameObject.SetActive(true);

        _playerCam.transform.position = transform.position;
        _playerCam.transform.rotation = transform.rotation;

        gameObject.SetActive(false);

        EventManager.Instance.Dispatch(GameEventTypes.OnGameplay, this, EventArgs.Empty);
    }

    public IEnumerator ResetAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ResetCamera();
    }
}
