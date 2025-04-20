using UnityEngine;

public class PowerManager : MonoBehaviour
{
    public enum Powers { OnRead, OnTime }

    public Powers currentPower = Powers.OnRead;

    [SerializeField] AnimationManager animManager;
    [SerializeField] Detection detection;

    [SerializeField] Camera playerCam;

    [SerializeField] private LayerMask ignoreMask;
    public float playerReach = 10f;
    float interactDistance = 5f;
    private ISwitcheable lastSwitcheable = null;
    private IRead lastReadeable = null;
    private Interactor lastInteractor = null;

    private bool Clicked;

    private void Start()
    {
        //detection.OnClick += Detect;
    }

    private void Update()
    {
    }

    void Detect()
    {
        RaycastHit hit;
        Ray ray = new Ray(playerCam.transform.position, playerCam.transform.forward);
        int layerMask = ~ignoreMask.value;

        ISwitcheable currentSwitcheable = null;
        IRead currentReadeable = null;
        Interactor currentInteractor = null;

        if (Physics.Raycast(ray, out hit, playerReach, layerMask))
        {
            if (hit.distance <= interactDistance)
            {
                if (hit.collider.TryGetComponent(out currentInteractor))
                {
                    currentInteractor.Aiming();

                    //if (onClick)
                    //{
                    //    currentInteractor.Interact();
                    //}
                }
            }

            if (currentPower == Powers.OnTime)
            {
                if (hit.collider.TryGetComponent(out currentSwitcheable))
                {
                    currentSwitcheable.Aiming();

                    //if (onClick)
                    //{
                    //    currentSwitcheable.Switch();
                    //}
                }
            }
            else if (currentPower == Powers.OnRead)
            {
                if (hit.collider.TryGetComponent(out currentReadeable))
                {
                    currentReadeable.Aiming();

                    //if (onClick && !GameManager.Instance.clickBlock)
                    //{
                    //    currentReadeable.Read();
                    //}
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

        if (lastInteractor != null && lastInteractor != currentInteractor)
        {
            lastInteractor.DisableOutline();
        }

        lastSwitcheable = currentSwitcheable;
        lastReadeable = currentReadeable;
        lastInteractor = currentInteractor;
    }

    public void PowersKeyBinding()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangePower(0);
            SendPowerSelection();
            //fadeUI.ShowUI((int)currentPower);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangePower(1);
            SendPowerSelection();
            //fadeUI.ShowUI((int)currentPower);
        }
    }

    void ChangePower(int power)
    {
        if ((int)currentPower == power)
            return;

        currentPower = (Powers)power;
    }

    public void SendPowerSelection()
    {
        //animManager.ChangeCurrentPower(currentPower);
    }
}
