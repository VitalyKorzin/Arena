using System.IO;
using UnityEngine;

public abstract class Saver : MonoBehaviour
{
    protected void SaveIntegerValue(string key, int value) 
        => PlayerPrefs.SetInt(key, value);

    protected bool TryLoadIntegerValue(string key, out int result)
    {
        result = default;

        if (PlayerPrefs.HasKey(key))
            result = PlayerPrefs.GetInt(key);

        return PlayerPrefs.HasKey(key) != false;
    }

    protected void SaveObject<T>(T obj, string fileName)
        => File.WriteAllText(GetPath(fileName), JsonUtility.ToJson(obj));

    protected bool TryLoadObject<T>(string fileName, out T obj)
    {
        obj = default;

        if (File.Exists(GetPath(fileName)))
            obj = JsonUtility.FromJson<T>(File.ReadAllText(GetPath(fileName)));

        return File.Exists(GetPath(fileName)) != false;
    }

    private string GetPath(string fileName)
        => Path.Combine(Application.dataPath, fileName);
}