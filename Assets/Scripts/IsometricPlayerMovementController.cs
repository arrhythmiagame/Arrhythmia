using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricPlayerMovementController : MonoBehaviour
{

    public float movementSpeed = 1f;
    public float dashMultiplier = 2f;
    IsometricCharacterRenderer isoRenderer;
    public Vector2 currentPos;
    public Vector2 newPos;
    public GameManager gm;
    Rigidbody2D rbody;

    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        isoRenderer = GetComponentInChildren<IsometricCharacterRenderer>();
        currentPos = rbody.position;
        newPos = currentPos;
        gm = GameManager.instance;

    }


    // Update is called once per frame
    void FixedUpdate()
    {
        currentPos = rbody.position;
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 inputVector = new Vector2(horizontalInput, verticalInput);
        inputVector = Vector2.ClampMagnitude(inputVector, 1);
        Vector2 movement = inputVector * movementSpeed * Time.deltaTime;
        if (Input.GetButtonDown("Dash"))
        {
            movement = inputVector * movementSpeed * dashMultiplier * Time.deltaTime;
        }
        newPos = currentPos + movement;
        rbody.MovePosition(newPos);
    }

    void CharacterDash(Vector2 inputVector)
    {
    }
}