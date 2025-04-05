using UnityEngine;

public class Detection : MonoBehaviour
{
    public float detectionRange = 10f;
    public Powers currentPower = Powers.OnRead;

    private bool onClick;
    private Material detMat;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            onClick = true;
        }

        Detect();

        onClick = false;

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0f)
        {
            ChangePower(1);
        }
        else if (scroll < 0f)
        {
            ChangePower(-1);
        }
    }

    void Detect()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * detectionRange, Color.red);

        if (Physics.Raycast(ray, out hit, detectionRange))
        {
            if (onClick)
            {
                switch (currentPower)
                {
                    case Powers.OnRead:
                        if (hit.collider.TryGetComponent(out IRead readable)) readable.Read();
                        break;

                    case Powers.OnTime:
                        if (hit.collider.TryGetComponent(out ISwitcheable switcheable)) switcheable.Switch();
                        break;

                }
            }
            else
            {
                switch (currentPower)
                {
                    case Powers.OnTime:
                        if (hit.collider.TryGetComponent(out ObjectTimeSwitching obj)) obj.Aiming();
                        break;
                }
            }
        }
        else
        {
            GameManager.Instance.isAimingAtObject = false;
        }
    }

    void ChangePower(int direction)
    {
        int maxPower = System.Enum.GetValues(typeof(Powers)).Length;
        int newPower = ((int)currentPower + direction + maxPower) % maxPower;

        currentPower = (Powers)newPower;

        Debug.Log("Poder cambiado a: " + currentPower);
    }
}

public enum Powers
{
    OnRead,
    OnTime
}