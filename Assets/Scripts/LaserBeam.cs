using System.Collections;
using UnityEngine;

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

    void Update()
    {
        _fireTimer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.E) && _fireTimer > fireRate)
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
            default:
                break;
        }
    }

    void ShootLaser()
    {
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

    private void ReflectLaser()
    {
        reflectRay = new Ray(playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0)), playerCamera.transform.forward);
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, laserSpawn.position);

        float remainingLength = defaultLength;
        RaycastHit hit;

        for (int i = 0; i < numOfReflection; i++)
        {
            if (Physics.Raycast(reflectRay.origin, reflectRay.direction, out hit, remainingLength, mirrorLayer))
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);
                remainingLength -= Vector3.Distance(reflectRay.origin, hit.point);

                reflectRay = new Ray(hit.point, Vector3.Reflect(reflectRay.direction, hit.normal));
            }
            else
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, reflectRay.origin + (reflectRay.direction * remainingLength));
            }
        }
    }
}
