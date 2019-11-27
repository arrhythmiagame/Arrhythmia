using UnityEngine;
using System.IO;
using System;
public class MainMenu : MonoBehaviour
{
    
    [SerializeField] string folderName;
    [SerializeField] string fileName;
    [SerializeField] string filePath;
    private void Awake()
    {
        string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        filePath = Path.Combine(documentsPath, folderName, fileName);
    }
    public void NewGame()
    {
        if (!File.Exists(filePath)) {
            StreamWriter writer = new StreamWriter(filePath, true);
        }

    }
}
