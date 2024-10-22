using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FloatingObject : MonoBehaviour

{

    public float floatStrength = 0.5f; 

    public float floatSpeed = 1.0f; 

    public float floatDistance = 0.5f;  

    public float moveSpeed = 2.0f;    

    public bool isMovingLeft = false;  

    public bool isMovingRight = true;   

    public Color startColor = Color.white; 

    public Color endColor = Color.red;  


    private Vector3 startPosition;

    private SpriteRenderer spriteRenderer;

    void Start()
    {

        startPosition = transform.position;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }



    void Update()
    {
        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatDistance;

        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        float moveDirection = 0;

        if (isMovingLeft)

        {

            moveDirection = -moveSpeed * Time.deltaTime;

        }

        if (isMovingRight)

        {

            moveDirection = moveSpeed * Time.deltaTime;

        }

        transform.position += new Vector3(moveDirection, 0, 0);

    }

}


