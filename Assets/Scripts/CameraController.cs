using Rewired;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera theCamera;
    [SerializeField] float minFrameValue = 0.05f;
    [SerializeField] float maxFrameValue = 0.95f;
    [SerializeField] float cameraSpeed = 0.1f;
    private Player player; // The Rewired Player
    private GameManager gm;
    [Header("Debug Only")]
    [SerializeField] Vector2 rightStickMoveVector;
    [SerializeField] float changeX;
    [SerializeField] float changeY;


    void Start()
    {
        gm = GameManager.instance;
        player = ReInput.players.GetPlayer(gm.playerId);

    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        ProcessInput();
    }
    private void GetInput()
    {
        rightStickMoveVector.x = player.GetAxis("RightStickHorizontal");
        rightStickMoveVector.y = player.GetAxis("RightStickVertical");
    }
    private void ProcessInput()
    {
        CinemachineFramingTransposer cameraFrame = theCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        if (Mathf.Abs(rightStickMoveVector.x) > 0.01f)
        {
            changeX = Mathf.Clamp(-rightStickMoveVector.x, minFrameValue, maxFrameValue);
            cameraFrame.m_ScreenX = Mathf.Lerp(cameraFrame.m_ScreenX, changeX, cameraSpeed);
        }
        else
        {
            cameraFrame.m_ScreenX = 0.5f;
        }
        if (Mathf.Abs(rightStickMoveVector.y) > 0.01f)
        {
            changeY = Mathf.Clamp(rightStickMoveVector.y, minFrameValue, maxFrameValue);
            cameraFrame.m_ScreenY = Mathf.Lerp(cameraFrame.m_ScreenY, changeY, cameraSpeed);
        }
        else
        {
            cameraFrame.m_ScreenY = 0.5f;
        }
    }
}
