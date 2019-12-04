using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System;
using System.Text;
public class CharacterMenu : MonoBehaviour
{
    [SerializeField] TMP_InputField nameInput;
    [SerializeField] GameObject nameDialog;
    [SerializeField] MenuInput characterInput;
    [SerializeField] Image skinImage;
    [SerializeField] Image clothesImage;
    [SerializeField] Image weaponImage;
    [SerializeField] Slider buildSlider;
    [SerializeField] Slider hairSlider;
    [SerializeField] Slider faceSlider;
    [SerializeField] Slider healthSlider;
    [SerializeField] Slider speedSlider;
    private int buildValue;
    private int saveIndex;
    private void Start()
    {
        string saveFileContents = File.ReadAllText(PlayerPrefs.GetString("SavePath"));
        saveIndex = saveFileContents.Split('\n').Length - 1;
        Debug.Log(saveIndex.ToString());
        PlayerPrefs.SetInt("CurrentSaveIndex", saveIndex);
    }
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
            CharacterObject thisCharacter = new CharacterObject();
            thisCharacter.saveIndex = saveIndex;
            thisCharacter.characterName = nameInput.text;
            thisCharacter.skinColor = skinImage.color;
            thisCharacter.clothesColor = clothesImage.color;
            thisCharacter.weaponColor = weaponImage.color;
            thisCharacter.maxHealth = Mathf.RoundToInt(thisCharacter.maxHealth * healthSlider.value);
            thisCharacter.health = Mathf.RoundToInt(thisCharacter.maxHealth / 2);
            thisCharacter.speedMultiplier = speedSlider.value;
            thisCharacter.buildType = buildValue;
            thisCharacter.hairType = Mathf.RoundToInt(hairSlider.value);
            thisCharacter.faceType = Mathf.RoundToInt(faceSlider.value);
            string json = JsonUtility.ToJson(thisCharacter);
            byte[] bytesToEncode = Encoding.UTF8.GetBytes(json);
            string encodedJSON = Convert.ToBase64String(bytesToEncode);
            string saveFilePath = PlayerPrefs.GetString("SavePath");
            StreamWriter writer = new StreamWriter(saveFilePath, true);
            writer.WriteLine(encodedJSON);
            writer.Close();
            SceneManager.LoadScene("StartingArea");
        }
    }
    public void CloseNameDialog()
    {
        nameDialog.SetActive(false);
        characterInput.enabled = true;
    }
    public void AdjustBuild()
    {
        buildValue = Mathf.RoundToInt(buildSlider.value);
        healthSlider.value = buildValue * 0.5f;
        speedSlider.value = 2 - healthSlider.value;
    }
    public void AdjustHair()
    {
        Debug.LogError("Hair not implemented yet!"); // TODO implement hair changes
    }
    public void AdjustFace()
    {
        Debug.LogError("Face not implemented yet!"); // TODO implement face changes
    }
}
