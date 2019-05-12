using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{

    public PlayerStats playerStats;
    const string folderName = "SaveData";
    const string fileExtension = ".dat";

    // Saving
    public void SaveGame() {
        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
        PlayerStats playerStats = player.playerStats;

        string folderPath = Path.Combine(Application.persistentDataPath, folderName);
        if (!Directory.Exists (folderPath))
            Directory.CreateDirectory (folderPath);            

        string dataPath = Path.Combine(folderPath, playerStats.characterName + fileExtension);    
        Debug.Log("data path: " + dataPath);   
        SaveCharacter (playerStats, dataPath);
    }

    static void SaveCharacter (PlayerStats data, string path)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        using (FileStream fileStream = File.Open (path, FileMode.OpenOrCreate))
        {
            binaryFormatter.Serialize (fileStream, data);
        }
    }

    // loading
    public void LoadGame() {
        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
        string[] filePaths = GetFilePaths ();
            Debug.Log("filePaths: " + filePaths[0]);  
        if(filePaths.Length > 0) {
            player.playerStats = LoadCharacter (filePaths[0]);
            Debug.Log("data: " + player.playerStats);   
        }

    }

    static PlayerStats LoadCharacter (string path)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        using (FileStream fileStream = File.Open (path, FileMode.Open))
        {
            return (PlayerStats)binaryFormatter.Deserialize (fileStream);
        }
    }

    static string[] GetFilePaths ()
    {
        string folderPath = Path.Combine(Application.persistentDataPath, folderName);

        return Directory.GetFiles (folderPath, fileExtension);
    }
}