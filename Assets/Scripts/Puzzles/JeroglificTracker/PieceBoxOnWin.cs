using Unity.VisualScripting;
using UnityEngine;

public class PieceBoxOnWin : MonoBehaviour
{
    [SerializeField] TrailManager manager;

    [SerializeField] GameObject closed;

    void Start()
    {
        manager.JeroglificAction += Win;
    }

    void Win(TrailManager manager)
    {
        closed.SetActive(false);
    }
}