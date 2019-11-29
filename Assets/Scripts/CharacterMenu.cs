using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
public class CharacterMenu : MonoBehaviour
{
    [SerializeField] TMP_InputField nameInput;
    [SerializeField] GameObject nameDialog;
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void CreateCharacter()
    {
        if(nameInput.text == "")
        {
            nameDialog.SetActive(true);
            nameDialog.GetComponentInChildren<Button>().Select();
        }
        else
        {
            Debug.Log("TODO Save info to text file"); // TODO Save info to text file
            SceneManager.LoadScene("StartingArea");
        }
    }
}
