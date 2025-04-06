using UnityEngine;

public class Detection : MonoBehaviour
{
    public float playerReach = 10f;
    public Powers currentPower = Powers.OnRead;

    private bool onClick;

    private ISwitcheable lastSwitcheable = null;

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
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);

        ISwitcheable currentSwitcheable = null;

        Debug.DrawRay(ray.origin, ray.direction * playerReach, Color.red, 2.0f);

        if (Physics.Raycast(ray, out hit, playerReach))
        {
            if (currentPower == Powers.OnTime)
            {
                if (hit.collider.TryGetComponent(out currentSwitcheable))
                {
                    currentSwitcheable.Aiming();

                    if (onClick)
                    {
                        currentSwitcheable.Switch();
                    }
                }
            }
            else if (currentPower == Powers.OnRead)
            {
                if (hit.collider.TryGetComponent(out IRead readable))
                {
                    if (onClick)
                    {
                        readable.Read();
                    }
                }
            }
        }

        if (lastSwitcheable != null && lastSwitcheable != currentSwitcheable)
        {
            lastSwitcheable.DisableOutline();
        }

        lastSwitcheable = currentSwitcheable;
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