using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    public GameManager gm;
    public GameObject laserPrefab;

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

        Invoke("TurnOffLaser", 0.1f);
    }

    void TurnOffLaser()
    {
        laserPrefab.SetActive(false);
    }
}
