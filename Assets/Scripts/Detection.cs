using UnityEngine;

public class Detection : MonoBehaviour
{
    public float playerReach = 10f;
    public Powers currentPower = Powers.OnRead;

    private bool onClick;

    private ISwitcheable lastSwitcheable = null;
    private IRead lastReadeable = null;

    [SerializeField] private LayerMask ignoreMask;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            onClick = true;
        }

        PowersKeyBinding();

        Detect();

        onClick = false;


    }

    void PowersKeyBinding()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangePower(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangePower(1);
        }
    }

    void Detect()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        int layerMask = ~ignoreMask.value;

        ISwitcheable currentSwitcheable = null;
        IRead currentReadeable = null;

        if (Physics.Raycast(ray, out hit, playerReach, layerMask))
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
                if (hit.collider.TryGetComponent(out currentReadeable))
                {
                    currentReadeable.Aiming();

                    if (onClick && !GameManager.Instance.clickBlock)
                    {
                        currentReadeable.Read();
                    }
                }
            }
        }

        if (lastSwitcheable != null && lastSwitcheable != currentSwitcheable)
        {
            lastSwitcheable.DisableOutline();
        }

        if (lastReadeable != null && lastReadeable != currentReadeable)
        {
            lastReadeable.DisableOutline();
        }

        lastSwitcheable = currentSwitcheable;
        lastReadeable = currentReadeable;
    }

    void ChangePower(int power)
    {
        if ((int)currentPower == power)
            return;

        currentPower = (Powers)power;
    }
}

public enum Powers
{
    OnRead,
    OnTime
}