using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;
    public Transform stone;

    float xRot;
    float yRot;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        float xMouse = Input.GetAxisRaw("Mouse X") * sensX;
        float yMouse = Input.GetAxisRaw("Mouse Y") * sensY;

        yRot += xMouse;
        xRot -= yMouse;
        xRot = Mathf.Clamp(xRot, -60f, 60f);

        transform.rotation = Quaternion.Euler(xRot, yRot, 0);

        orientation.rotation = Quaternion.Euler(0, yRot, 0);
        transform.parent.parent.rotation = Quaternion.Euler(0, yRot, 0);
    }
}
