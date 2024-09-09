using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Slider SensX;
    public Slider SensY;

    public Transform orientation;
    public Transform stone;

    float xRot;
    float yRot;

    public bool menuPressed;
    [SerializeField] GameObject pauseMenu;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        sensX = SensX.value; 
        sensY = SensY.value;

        float xMouse = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float yMouse = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRot += xMouse;
        xRot -= yMouse;
        xRot = Mathf.Clamp(xRot, -60f, 60f);

        transform.rotation = Quaternion.Euler(xRot, yRot, 0);

        orientation.rotation = Quaternion.Euler(0, yRot, 0);
        transform.parent.parent.rotation = Quaternion.Euler(0, yRot, 0);
    }
}
