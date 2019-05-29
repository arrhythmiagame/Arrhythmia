using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricPlayerMovementController : MonoBehaviour
{

    public float movementSpeed = 1f;
    IsometricCharacterRenderer isoRenderer;
    public Vector2 currentPos;
    public Vector2 newPos;

    Rigidbody2D rbody;

    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        isoRenderer = GetComponentInChildren<IsometricCharacterRenderer>();
        currentPos = rbody.position;
        newPos = currentPos;

    }


    // Update is called once per frame
    void FixedUpdate()
    {
        currentPos = rbody.position;
        if (newPos == currentPos)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
            inputVector = Vector2.ClampMagnitude(inputVector, 1);
            Vector2 movement = inputVector * movementSpeed;
            if (GameManager.instance.inputAllowed)
            {
                newPos = currentPos + movement;
            }
        }
        else
        {
            float xDifference = Mathf.Abs(newPos.x - currentPos.x);
            float yDifference = Mathf.Abs(newPos.y - currentPos.y);
            Debug.Log(xDifference);
            if (xDifference < .1 && yDifference < .1)
            {
                newPos = currentPos;
            }
            else
            {
                float movementTime = 0.1f;
                float lerpX = Mathf.Lerp(currentPos.x, newPos.x, movementTime);
                float lerpY = Mathf.Lerp(currentPos.y, newPos.y, movementTime);
                Vector2 lerpPos = new Vector2(lerpX, lerpY);
                rbody.MovePosition(lerpPos);
            }
        }
    }
}
