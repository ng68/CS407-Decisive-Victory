using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveLoad : MonoBehaviour
{
    public string file = "savefile.txt";
    public List<savedata> saves;
    public void Save()
    {
        Debug.Log("Trying to write file.");
        string json = JsonUtility.ToJson(saves);
        WriteToFile(file, json);
    }
    public void Load()
    {
        saves = new List<savedata>();
        string json = ReadFromFile(file);
        JsonUtility.FromJsonOverwrite(json, saves);
    }

    public void WriteToFile(string fileName, string json)
    {
        string path = GetFilePath(fileName);
        FileStream fileStream = new FileStream(path, FileMode.Create);
        using (StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(json);
        }
    }
    private string GetFilePath(string fileName)
    {
        return UnityEngine.Application.persistentDataPath+"/"+fileName;
    }

    private string ReadFromFile(string fileName)
    {
        string path = GetFilePath(fileName);
        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string json = reader.ReadToEnd();
                return json;
            }
        }
        else
        {
            Debug.Log("File not found!");
            return "";
        }
    }

}


[System.Serializable]
public class savedata
{
    public string charname;
    public int levelswon;
}
