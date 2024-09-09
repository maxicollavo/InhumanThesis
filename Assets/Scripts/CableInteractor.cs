using UnityEngine;

public class CableInteractor : MonoBehaviour, Interactor
{
    [SerializeField] GameObject objectToInteract;
    public Animation Anim1;
    public Animation Anim2;

    public void Interact()
    {
        var coll = GetComponent<BoxCollider>();
        if (coll != null)
            coll.enabled = false;

        GameManager.Instance.cableCounter++;

        if (GameManager.Instance.cableCounter == 2)
            objectToInteract.SetActive(false);
    }
}
