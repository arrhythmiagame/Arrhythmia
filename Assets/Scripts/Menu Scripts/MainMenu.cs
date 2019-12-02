using UnityEngine;
using System.IO;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    [Header("File Locations")]
    [SerializeField] string folderName;
    [SerializeField] string saveFileName;
    [Header("Debug Only")]
    [SerializeField] string documentsPath;
    [SerializeField] string folderPath;
    [SerializeField] string saveFilePath;
    [Header("References")]
    [SerializeField] GameObject loadButton;
    [SerializeField] GameObject startMenu;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject loadMenu;
    [SerializeField] GameObject optionsMenu;
    [SerializeField] GameObject audioMenu;
    [SerializeField] GameObject controlsMenu;
    [Header("Options to Save")]
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider beatSlider;
    [SerializeField] Slider ambientSlider;
    private void Start()
    {
        InitializeGame();
    }
    private void InitializeGame()
    {
        // Setting up folder paths
        documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        folderPath = Path.Combine(documentsPath, folderName);
        // Creating folder to save everything in
        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);
        // Creating save data file
        saveFilePath = Path.Combine(folderPath, saveFileName);
        if (!File.Exists(saveFilePath))
            File.Create(saveFilePath);
        PlayerPrefs.SetString("SavePath", saveFilePath);
        // Checking if previous saves exist
        var saveFileContents = File.ReadAllText(saveFilePath);
        if (saveFileContents != "")
        {
            loadButton.SetActive(true);
        }
        LoadOptions();
        ActivateStartMenu();
    }
    // Create a new game
    public void NewGame()
    {
        SceneManager.LoadScene("CharacterCustomization");
    }
    // Load a new game
    public void LoadGame()
    {
        CloseOutMenus();
        loadMenu.SetActive(true);
        Debug.LogError("Not implemented yet."); // TODO add load code
    }
    // Save Options to PlayerPrefs 
    public void SaveOptions()
    {
        PlayerPrefs.SetFloat("masterVolume", masterSlider.value);
        PlayerPrefs.SetFloat("beatVolume", beatSlider.value);
        PlayerPrefs.SetFloat("ambientVolume", ambientSlider.value);
    }
    public void LoadOptions()
    {
        masterSlider.value = PlayerPrefs.GetFloat("masterVolume", 0.5f);
        beatSlider.value = PlayerPrefs.GetFloat("beatVolume", 0.5f);
        ambientSlider.value = PlayerPrefs.GetFloat("ambientVolume", 0.5f);
    }
    public void GetControlsLayout()
    {
        Debug.LogError("Not implemented yet."); // TODO add controls layout code
    }
    public void CloseOutMenus()
    {
        startMenu.SetActive(false);
        mainMenu.SetActive(false);
        loadMenu.SetActive(false);
        optionsMenu.SetActive(false);
        audioMenu.SetActive(false);
        controlsMenu.SetActive(false);
    }
    public void ActivateStartMenu()
    {
        CloseOutMenus();
        startMenu.SetActive(true);
    }
    public void ActivateMainMenu()
    {
        CloseOutMenus();
        mainMenu.SetActive(true);
    }
    public void ActivateOptionsMenu()
    {
        CloseOutMenus();
        optionsMenu.SetActive(true);
    }
    public void ActivateAudioMenu()
    {
        CloseOutMenus();
        audioMenu.SetActive(true);
    }
    public void ActivateControlsMenu()
    {
        CloseOutMenus();
        controlsMenu.SetActive(true);
    }
}
