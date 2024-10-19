using UnityEngine;
using UnityEngine.UI;

public class PowerChange : MonoBehaviour
{
    public static PowerChange Instance;

    [SerializeField] Image laserTransparency;
    [SerializeField] Image dimTransparency;
    [SerializeField] Image crosshair;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Color laserColor = laserTransparency.color;
        laserColor.a = 1f;
        laserTransparency.color = laserColor;

        Color dimColor = dimTransparency.color;
        dimColor.a = 50f / 255f;
        dimTransparency.color = dimColor;
    }

    public void PowerChangeCall(int power)
    {
        if (power == 0) LaserChange();
        else if (power == 1) DimensionChange();
    }

    private void LaserChange()
    {
        GameManager.Instance.ChangeState(PowerStates.OnLaser);

        crosshair.color = new Color32(255, 0, 0, 255);

        Color laserColor = laserTransparency.color;
        laserColor.a = 1f;
        laserTransparency.color = laserColor;

        Color dimColor = dimTransparency.color;
        dimColor.a = 50f / 255f;
        dimTransparency.color = dimColor;
    }

    private void DimensionChange()
    {
        GameManager.Instance.ChangeState(PowerStates.OnDimension);

        crosshair.color = new Color32(255, 0, 233, 255);

        Color dimColor = dimTransparency.color;
        dimColor.a = 1f;
        dimTransparency.color = dimColor;

        Color laserColor = laserTransparency.color;
        laserColor.a = 50f / 255f;
        laserTransparency.color = laserColor;
    }
}