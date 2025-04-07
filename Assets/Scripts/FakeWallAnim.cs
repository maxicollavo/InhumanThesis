using UnityEngine;

public class FakeWallAnim : MonoBehaviour
{
    [SerializeField] FakeWallInteractor fakeWall;

    public void ActivateColumns()
    {
        StartCoroutine(fakeWall.OnCinematicMethod());
    }
}