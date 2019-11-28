using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;
using System;
public class MenuInput : MonoBehaviour
{
    [SerializeField] GameObject[] inputs;
    [SerializeField] Button backButton;
    private Button theButton;
    private Slider theSlider;
    private int inputIndex = 0;
    // Rewired Stuff
    [SerializeField] int playerId = 0;
    private Player player; 
    private bool menuUp;
    private bool menuDown;
    private bool confirm;
    private bool cancel;
    private void Start()
    {
        SelectInput();
        player = ReInput.players.GetPlayer(playerId);
    }
    private void OnEnable()
    {
        inputIndex = 0;
        SelectInput();
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
        confirm = player.GetButtonDown("Confirm");
        cancel = player.GetButtonDown("Cancel");
    }
    private void ProcessInput()
    {
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
    private void SelectPreviousButton()
    {
        inputIndex -= 1;
        if (inputIndex < 0)
        {
            inputIndex = inputs.Length - 1;
        }
        while (!inputs[inputIndex].activeSelf)
        {
            inputIndex -= 1;
            if (inputIndex < 0)
            {
                inputIndex = inputs.Length - 1;
            }
        }
        SelectInput();
    }
    void SelectNextButton()
    {
        inputIndex += 1;
        if(inputIndex >= inputs.Length)
        {
            inputIndex = 0;
        }
        while (!inputs[inputIndex].activeSelf)
        {
            inputIndex += 1;
            if (inputIndex >= inputs.Length)
            {
                inputIndex = 0;
            }
        }
        SelectInput();
    }
    void SelectInput()
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
