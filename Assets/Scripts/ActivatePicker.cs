using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePicker : MonoBehaviour
{
    public GameObject thisPicker;
    private GameObject[] allPickers;
    public void TogglePicker()
    {
        if (thisPicker.activeSelf)
        {
            thisPicker.SetActive(false);
        }
        else
        {
            allPickers = GameObject.FindGameObjectsWithTag("Picker");
            foreach (GameObject thePicker in allPickers)
            {
                thePicker.SetActive(false);
            }
            thisPicker.SetActive(true);

        }
    }
}
