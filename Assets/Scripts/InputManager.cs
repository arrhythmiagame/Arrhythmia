using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class InputManager : MonoBehaviour
{
    public float horizontalValue;
    public float verticalValue;
    public float leftStickHorizontal;
    public float leftStickVertical;
    public float rightStickHorizontal;
    public float rightStickVertical;
    public bool dashValue;
    public bool attackValue;
    public bool blockValue;
    public bool ultimateValue;
    public bool pauseValue;
    public bool jsButton0;
    public string[] controllerNames;


    void Update()
    {
        horizontalValue = Input.GetAxis("Horizontal");
        verticalValue = Input.GetAxis("Vertical");
        leftStickHorizontal = Input.GetAxis("Leftstick Horizontal");
        leftStickVertical = Input.GetAxis("Leftstick Vertical");
        rightStickHorizontal = Input.GetAxis("Rightstick Horizontal");
        rightStickVertical = Input.GetAxis("Rightstick Vertical");
        dashValue = Input.GetButton("Dash");
        attackValue = Input.GetButton("Attack");
        blockValue = Input.GetButton("Block");
        ultimateValue = Input.GetButton("Ultimate");
        pauseValue = Input.GetButton("Cancel");
        controllerNames = Input.GetJoystickNames();
    }
}
