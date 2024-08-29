using System.Collections;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] GameManager gm;
    [SerializeField] Camera playerCamera;
    [SerializeField] Transform laserSpawn;
    [SerializeField] float gunRange = 50f;
    [SerializeField] float fireRate = 0.2f;

    private float _fireTimer;
    private WaitForSeconds wfs = new WaitForSeconds(0.05f);

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
        lineRenderer.SetPosition(0, laserSpawn.position);
        Vector3 rayOirigin = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(rayOirigin, playerCamera.transform.forward, out hit, gunRange))
        {
            lineRenderer.SetPosition(1, hit.point);
            var interactor = hit.collider.GetComponent<Interactor>();

            if (interactor != null)
            {
                interactor.Interact();
            }
        }
        else
        {
            lineRenderer.SetPosition(1, rayOirigin + (playerCamera.transform.forward * gunRange));
        }

        StartCoroutine(ShootLaserCor());
    }

    IEnumerator ShootLaserCor()
    {
        lineRenderer.enabled = true;
        yield return wfs;
        lineRenderer.enabled = false;
    }
}
