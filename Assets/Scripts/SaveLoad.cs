using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;   
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
public class SaveLoad : MonoBehaviour
{
    public string file = "savefile.txt";
    public saveRoot saves;
    public void Save()
    {
        Debug.LogWarning("Trying to write file.");
        Debug.LogWarning(saves.saves[0].charname);
        /*var jsonString = JsonUtility.ToJson<saveRoot>(saves);
        Debug.LogWarning(jsonString);
        WriteToFile(file, jsonString);
        */
    }
    public void Load()
    {
        string json = ReadFromFile(file);
        saves = JsonUtility.FromJson<saveRoot>(json);
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
public class savedata : IComparable
{
    [SerializeField]
    public string charname;
    [SerializeField]
    public int levelswon;

    [SerializeField]
    public int score;


    public int CompareTo(object obj)
    {
        return this.score.CompareTo((obj as savedata).score);
    }
}


public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}

[System.Serializable]
public class saveRoot
{
    public savedata[] saves;
}