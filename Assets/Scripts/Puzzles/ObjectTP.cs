using UnityEngine;

public class ObjectTP : MonoBehaviour, ITeleportable
{
    private bool onTeleport;

    [SerializeField] TPColours assetColour;
    [SerializeField] Transform spawnPoint;

    public void Interact()
    {
        onTeleport = !onTeleport;

        if (onTeleport)
        {
            TPManager.Instance.coloursList.Add(assetColour);
            int randomIndex = GetRandomRealIndex();
            transform.position = GameManager.Instance.spawnerReal[randomIndex].position;
        }
        else
        {
            TPManager.Instance.coloursList.Remove(assetColour);
            transform.position = spawnPoint.position;
        }

        TPManager.Instance.CheckForColours(onTeleport, assetColour);
    }

    int GetRandomRealIndex()
    {
        return Random.Range(0, GameManager.Instance.spawnerReal.Count);

    }
}
