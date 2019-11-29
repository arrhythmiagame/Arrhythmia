using UnityEngine;
using UnityEngine.UI;
using Rewired;

public class PickerInput : MonoBehaviour
{
    [SerializeField] MenuInput mainMenu;
    [SerializeField] GameObject[] inputs;
    [SerializeField] Button backButton;
    [SerializeField] float moveSpeed = 1f;
    private Button theButton;
    private Slider theSlider;
    private BoxSlider theBoxSlider;
    private int inputIndex = 0;
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
    private void OnEnable()
    {
        SelectInput();
        mainMenu.ToggleInputAllowed();
        player = ReInput.players.GetPlayer(playerId);
    }
    private void OnDisable()
    {
        mainMenu.ToggleInputAllowed();
    }
    private void Update()
    {
        GetInput();
        ProcessInput();
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
        if (menuDown || menuRight)
        {
            SelectNextButton();
        }
        if (menuUp || menuLeft)
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
            if (backButton != null)
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
        if (theBoxSlider != null)
        {
            theBoxSlider.value += movement.x;
            theBoxSlider.valueY += movement.y;
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
        if (inputIndex >= inputs.Length)
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
        if (inputs[inputIndex].GetComponents<Slider>().Length > 0)
        {
            theSlider = inputs[inputIndex].GetComponent<Slider>();
            theSlider.Select();
        }
        else
        {
            theSlider = null;
        }
        if (inputs[inputIndex].GetComponents<BoxSlider>().Length > 0)
        {
            theBoxSlider = inputs[inputIndex].GetComponent<BoxSlider>();
            theBoxSlider.Select();
        }
        else
        {
            theBoxSlider = null;
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

}
