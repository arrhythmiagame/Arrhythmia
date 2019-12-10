using UnityEngine;
using Rewired;

public class IsometricPlayerMovementController : MonoBehaviour
{

    [SerializeField] GameManager gm;
    private Rigidbody2D rbody;
    [Header("Rewired Stuff")]
    // The movement speed of this character
    [SerializeField] float moveSpeed = 3.0f;
    [SerializeField] float dashMultiplier = 2f;

    private Player player; // The Rewired Player
    private Vector3 moveVector;
    private bool dash;
    private Vector2 currentPos;
    private Vector2 newPos;
    private string state = "idle";


    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        currentPos = rbody.position;
        newPos = currentPos;

    }

    private void Start()
    {
        gm = GameManager.instance;
        player = ReInput.players.GetPlayer(gm.playerId);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        GetInput();
        ProcessInput();
    }

    private void GetInput()
    {
        // Get the input from the Rewired Player. All controllers that the Player owns will contribute, so it doesn't matter
        // whether the input is coming from a joystick, the keyboard, mouse, or a custom controller.

        moveVector.x = player.GetAxis("Move Horizontal");
        moveVector.y = player.GetAxis("Move Vertical");
        dash = player.GetButtonDown("Dash");
    }

    private void ProcessInput()
    {
        // Process movement
        if (moveVector.x != 0.0f || moveVector.y != 0.0f)
        {
            state = "run";
            Move(moveVector * moveSpeed * Time.deltaTime);
        }

        // Process dash
        if (dash)
        {
            if (gm.CheckInputAllowed())
            {
                state = "dash";
                Move(moveVector * moveSpeed * dashMultiplier * Time.deltaTime);
                gm.DisableInput();
            }
        }
    }
    void Move(Vector2 movement)
    {
        currentPos = rbody.position;
        newPos = currentPos + movement;
        if (state == "dash")
        {
            var linePositions = new Vector3[2];
            var lineStart = new Vector3(0, 0, 0);
            var lineEnd = new Vector3(currentPos.x - newPos.x, currentPos.y - newPos.y, 0);
            linePositions[0] = lineEnd;
            linePositions[1] = lineStart;
        }
        rbody.MovePosition(newPos);
    }
}