using UnityEngine;

public class Detection : MonoBehaviour
{
    public float playerReach = 10f;
    public Powers currentPower = Powers.OnRead;

    private bool onClick;

    private ISwitcheable lastSwitcheable = null;

    [SerializeField] private LayerMask ignoreMask;

    [SerializeField] private CursorManager cursor;
    private bool isCursorOpen = false;

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
        int layerMask = ~ignoreMask.value;

        ISwitcheable currentSwitcheable = null;

        if (Physics.Raycast(ray, out hit, playerReach, layerMask))
        {
            if (currentPower == Powers.OnTime)
            {
                if (hit.collider.TryGetComponent(out currentSwitcheable))
                {
                    currentSwitcheable.Aiming();
                    ChangeCursor(true);

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
                    ChangeCursor(true);
                    if (onClick)
                    {
                        readable.Read();
                    }
                }
            }
        }
        ChangeCursor(false);

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

    void ChangeCursor(bool shouldBeOpen)
    {
        if (shouldBeOpen && !isCursorOpen)
        {
            cursor.SetToOpen();
            isCursorOpen = true;
        }
        else if (!shouldBeOpen && isCursorOpen)
        {
            cursor.SetToIdle();
            isCursorOpen = false;
        }
    }
}

public enum Powers
{
    OnRead,
    OnTime
}