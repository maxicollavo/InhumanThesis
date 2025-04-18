using UnityEngine;

public class FollowMouseClick : MonoBehaviour
{
    [SerializeField] Camera cam;
    [HideInInspector]
    public ParticleSystem particle;
    float fixedZ;

    private void Awake()
    {
        particle = GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        fixedZ = transform.position.z;
    }

    void Update()
    {
        if (!gameObject.activeInHierarchy) return;

        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Mathf.Abs(cam.transform.position.z - fixedZ);

            Vector3 worldPos = cam.ScreenToWorldPoint(mousePos);
            worldPos.z = fixedZ;

            transform.position = worldPos;
        }
    }
}