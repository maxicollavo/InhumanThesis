using System.Collections;
using UnityEngine;
using System;

public class LaserBeam : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] GameManager gm;
    [SerializeField] Camera playerCamera;
    [SerializeField] Transform laserSpawn;
    [SerializeField] float gunRange = 20f;
    [SerializeField] float fireRate = 0.2f;
    private float _fireTimer;
    public int numOfReflection = 6;
    Ray reflectRay;
    public float defaultLength = 50;
    public LayerMask mirrorLayer;

    #region Shooting
    [SerializeField] AudioSource laserSound;
    #endregion Shooting

    void Update()
    {
        _fireTimer += Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && _fireTimer > fireRate && gm.canShoot)
        {
            ActivatePower();
        }
    }

    void ActivatePower()
    {
        _fireTimer = 0;

        switch (gm.state)
        {
            case PowerStates.OnLaser:
                ShootLaser();
                break;
            case PowerStates.OnDimension:
                Debug.Log("Cambia de poder a dimension change");
                break;
            default:
                break;
        }
    }

    void ShootLaser()
    {
        laserSound.Play();
        Vector3 rayOirigin = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(rayOirigin, playerCamera.transform.forward, out hit, gunRange))
        {
            var interactor = hit.collider.GetComponent<Interactor>();
            if (interactor != null)
            {
                interactor.Interact();
            }
        }
        else hit.point = rayOirigin + playerCamera.transform.forward * 20f;

        StartCoroutine(ShootLaserCor(hit.point));
        StartCoroutine(ShootTimer());
    }

    IEnumerator ShootTimer()
    {
        gm.canShoot = false;
        yield return new WaitForSeconds(0.25f);
        gm.canShoot = true;
    }

    IEnumerator ShootLaserCor(Vector3 hit)
    {
        float elapsedTime = 0;
        lineRenderer.enabled = true;
        while (elapsedTime <= 0.05f)
        {
            lineRenderer.SetPosition(0, laserSpawn.position);
            Vector3 rayOirigin = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            lineRenderer.SetPosition(1, hit);
            yield return new WaitForEndOfFrame();
            elapsedTime += Time.deltaTime;
        }
        lineRenderer.enabled = false;
    }
}
