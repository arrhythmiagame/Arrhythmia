using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Rewired;
using TMPro;
public class MenuInput : MonoBehaviour
{
    [SerializeField] GameObject[] inputs;
    [SerializeField] Button backButton;
    [SerializeField] float moveSpeed = 1f;
    private Button theButton;
    private Slider theSlider;
    private TMP_InputField theInput;
    private int inputIndex = 0;
    private bool allowInput = true;
    private Vector2 rightStickVector;
    // Rewired Stuff
    [SerializeField] int playerId = 0;
    private Player player; 
    private bool menuUp;
    private bool menuDown;
    private bool menuRight;
    private bool menuLeft;
    private bool confirm;
    private bool cancel;
    private void Start()
    {
        player = ReInput.players.GetPlayer(playerId);
        inputIndex = 0;
        SelectInput();
    }
    private void OnEnable()
    {
        inputIndex = 0;
        SelectInput();
    }
    private void Update()
    {
        if (allowInput)
        {
            GetInput();
            ProcessInput();
        }
    }
    private void GetInput()
    {
        menuUp = player.GetButtonDown("MenuUp");
        menuDown = player.GetButtonDown("MenuDown");
        menuRight = player.GetButtonDown("MenuRight");
        menuLeft = player.GetButtonDown("MenuLeft");
        confirm = player.GetButtonDown("Confirm");
        cancel = player.GetButtonDown("Cancel");
        rightStickVector.x = player.GetAxis("RightStickHorizontal");
        rightStickVector.y = player.GetAxis("RightStickVertical");
    }
    private void ProcessInput()
    {
        if (rightStickVector.x != 0.0f || rightStickVector.y != 0.0f)
        {
            MoveSlider(rightStickVector * moveSpeed * Time.deltaTime);
        }
        if (menuRight)
        {
            MoveSlider(new Vector2(1, 0) * moveSpeed * Time.deltaTime);
        }
        if (menuLeft)
        {
            MoveSlider(new Vector2(-1, 0) * moveSpeed * Time.deltaTime);
        }
        if (menuDown)
        {
            SelectNextButton();
        }
        if (menuUp)
        {
            SelectPreviousButton();
        }
        if (confirm)
        {
            if (theButton != null)
            {
                theButton.onClick.Invoke();
            }
        }
        if (cancel)
        {
            if(backButton != null)
            {
                backButton.onClick.Invoke();
            }
        }
    }
    private void MoveSlider(Vector2 movement)
    {
        if (theSlider != null)
        {
            theSlider.value += movement.x;
        }
    }
    private void SelectPreviousButton()
    {
        inputIndex -= 1;
        if (inputIndex < 0)
        {
            inputIndex = inputs.Length - 1;
        }
        while (!inputs[inputIndex].activeInHierarchy)
        {
            inputIndex -= 1;
            if (inputIndex < 0)
            {
                inputIndex = inputs.Length - 1;
            }
        }
        SelectInput();
    }
    private void SelectNextButton()
    {
        inputIndex += 1;
        if(inputIndex >= inputs.Length)
        {
            inputIndex = 0;
        }
        while (!inputs[inputIndex].activeInHierarchy)
        {
            inputIndex += 1;
            if (inputIndex >= inputs.Length)
            {
                inputIndex = 0;
            }
        }
        SelectInput();
    }
    private void SelectInput()
    {
        EventSystem.current.SetSelectedGameObject(null);
        if (inputs[inputIndex].GetComponents<Slider>().Length > 0)
        {
            theSlider = inputs[inputIndex].GetComponent<Slider>();
            theSlider.Select();
        }
        else
        {
            theSlider = null;
        }
        if (inputs[inputIndex].GetComponents<TMP_InputField>().Length > 0)
        {
            theInput = inputs[inputIndex].GetComponent<TMP_InputField>();
            theInput.Select();
        }
        else
        {
            theInput = null;
        }
        if (inputs[inputIndex].GetComponents<Button>().Length > 0)
        {
            theButton = inputs[inputIndex].GetComponent<Button>();
            theButton.Select();
        }
        else
        {
            theButton = null;
        }
    }
    public void ToggleInputAllowed()
    {
        if (allowInput)
        {
            allowInput = false;
        }
        else
        {
            allowInput = true;
        }
    }
    public void ActivateKeyboard()
    {
        if (ReInput.controllers.joystickCount == 0)
        {
            return; // no joysticks, nothing to do
        }
        else
        {
            Debug.Log("Need to implement keyboard"); // TODO implement keyboard
        }
    }
}
