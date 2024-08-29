using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    public GameManager gm;
    public GameObject laserPrefab;
    public Camera playerCamera;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ActivatePower();
        }
    }

    void ActivatePower()
    {
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
        laserPrefab.SetActive(true);

        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        float maxDistance = 100f;

        Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.red, 1.0f);

        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            if (hit.collider.CompareTag("Switch"))
            {
                Debug.Log("Switch hit by laser!");
            }
        }

        Invoke("TurnOffLaser", 0.1f);
    }

    void TurnOffLaser()
    {
        laserPrefab.SetActive(false);
    }
}
