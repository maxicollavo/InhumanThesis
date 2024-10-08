using UnityEngine;

public class ObjectTP : MonoBehaviour, ITeleportable
{
    private bool onTeleport;

    [SerializeField] TPColours assetColour;

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
            int randomIndex = GetRandomUpsideIndex();
            transform.position = GameManager.Instance.spawnerUpside[randomIndex].position;
        }

        TPManager.Instance.CheckForColours(onTeleport, assetColour);
    }

    int GetRandomUpsideIndex()
    {
        return Random.Range(0, GameManager.Instance.spawnerUpside.Count);
    }

    int GetRandomRealIndex()
    {
        return Random.Range(0, GameManager.Instance.spawnerReal.Count);

    }
}
