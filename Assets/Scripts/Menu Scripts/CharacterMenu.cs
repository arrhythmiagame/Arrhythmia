using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
public class CharacterMenu : MonoBehaviour
{
    [SerializeField] TMP_InputField nameInput;
    [SerializeField] GameObject nameDialog;
    [SerializeField] MenuInput characterInput;
    [SerializeField] Image skinImage;
    [SerializeField] Image clothesImage;
    [SerializeField] Image weaponImage;
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
            characterInput.enabled = false;
        }
        else
        {
            string saveFilePath = PlayerPrefs.GetString("SavePath");
            CharacterObject thisCharacter = new CharacterObject();
            thisCharacter.characterName = nameInput.text;
            thisCharacter.skinColor = skinImage.color;
            thisCharacter.clothesColor = clothesImage.color;
            thisCharacter.weaponColor = weaponImage.color;
            string json = JsonUtility.ToJson(thisCharacter);
            StreamWriter writer = new StreamWriter(saveFilePath, true);
            writer.WriteLine(json);
            writer.Close();
            SceneManager.LoadScene("StartingArea");
        }
    }
    public void CloseNameDialog()
    {
        nameDialog.SetActive(false);
        characterInput.enabled = true;
    }
}
