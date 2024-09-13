using UnityEngine;

public class CableInteractor : MonoBehaviour, Interactor
{
    [SerializeField] GameObject objectToInteract;
    [SerializeField] Animator anim;
    [SerializeField] ParticleSystem part;

    private void Update()
    {
        if (GameManager.Instance.cableCounter == 2)
        {
            objectToInteract.SetActive(false);
            part.Stop();
        }
    }

    public void Interact()
    {
        var coll = GetComponent<BoxCollider>();
        if (coll != null)
            coll.enabled = false;

        part.Play();
        anim.SetBool("OnAction", true);
    }

    public void AnimFinish()
    {
        GameManager.Instance.cableCounter++;
    }
}
