using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
public class SaveLoad : MonoBehaviour
{
    public string file = "savefile.txt";
    public List<savedata> saves;
    public void Save()
    {
        Debug.LogWarning("Trying to write file.");
        Debug.LogWarning(saves[0].charname);
        var jsonString = JsonConvert.SerializeObject(saves);
        Debug.LogWarning(jsonString);
        WriteToFile(file, jsonString);
    }
    public void Load()
    {
        saves = new List<savedata>();
        string json = ReadFromFile(file);
        saves = JsonConvert.DeserializeObject<List<savedata>>(json);
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
    [SerializeField]
    public string charname;
    [SerializeField]
    public int levelswon;
}
