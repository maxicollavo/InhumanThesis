using UnityEngine;

public class PowerChange : MonoBehaviour
{
    public void LaserChange()
    {
        GameManager.Instance.ChangeState(PowerStates.OnLaser);
    }

    public void DimensionChange()
    {
        GameManager.Instance.ChangeState(PowerStates.OnDimension);
    }
}