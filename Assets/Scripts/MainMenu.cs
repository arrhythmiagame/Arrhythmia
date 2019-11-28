using UnityEngine;
using System.IO;
using System;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    [Header("File Locations")]
    [SerializeField] string folderName;
    [SerializeField] string saveFileName;
    [SerializeField] string optionsFileName;
    private string documentsPath;
    private string folderPath;
    [Header("References")]
    [SerializeField] GameObject loadButton;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject loadMenu;
    [SerializeField] GameObject optionsMenu;
    [SerializeField] GameObject audioMenu;
    [SerializeField] GameObject controlsMenu;
    private void Awake()
    {
        ActivateMainMenu();
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
        string saveFilePath = Path.Combine(folderPath, saveFileName);
        if (!File.Exists(saveFilePath))
            File.Create(saveFilePath);
        // Creating options data file
        string optionsFilePath = Path.Combine(folderPath, optionsFileName);
        if (!File.Exists(optionsFilePath))
            File.Create(optionsFilePath);
        // Checking if previous saves exist
        var saveFileContents = File.ReadAllText(saveFilePath);
        if (saveFileContents != "")
        {
            loadButton.SetActive(true);
        }
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
    public void SaveOptions()
    {
        Debug.LogError("Not implemented yet."); // TODO add save options code
    }
    public void GetControlsLayout()
    {
        Debug.LogError("Not implemented yet."); // TODO add controls layout code
    }
    public void CloseOutMenus()
    {
        mainMenu.SetActive(false);
        loadMenu.SetActive(false);
        optionsMenu.SetActive(false);
        audioMenu.SetActive(false);
        controlsMenu.SetActive(false);
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
