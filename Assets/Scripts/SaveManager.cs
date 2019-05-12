using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{

    public PlayerStats playerStats;
    const string folderName = "SaveData";
    const string fileExtension = ".dat";

    public void SaveGame() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        //PlayerStats playerStats = player.getPlayerStats();
        PlayerStats playerStats = player.GetComponent<PlayerStats>();

        string folderPath = Path.Combine(Application.persistentDataPath, folderName);
        if (!Directory.Exists (folderPath))
            Directory.CreateDirectory (folderPath);            

        string dataPath = Path.Combine(folderPath, playerStats.characterName + fileExtension);       
        SaveCharacter (playerStats, dataPath);
    }

    public static void LoadGame() {
        string[] filePaths = GetFilePaths ();
            
        if(filePaths.Length > 0) {
            PlayerStats playerStats = LoadCharacter (filePaths[0]);
        }
    }

    static void SaveCharacter (PlayerStats data, string path)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        using (FileStream fileStream = File.Open (path, FileMode.OpenOrCreate))
        {
            binaryFormatter.Serialize (fileStream, data);
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