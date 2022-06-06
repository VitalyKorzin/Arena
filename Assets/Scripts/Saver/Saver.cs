using System.IO;
using UnityEngine;

public abstract class Saver : MonoBehaviour
{
    private delegate T LoadObject<T>(string key);

    protected void SaveIntegerValue(string key, int value) 
        => PlayerPrefs.SetInt(key, value);

    protected bool TryLoadIntegerValue(string key, out int result) 
        => TryGetObject(PlayerPrefs.HasKey(key), key, LoadFromPlayerPrefs, out result);

    protected void SaveObject<T>(T obj, string fileName)
        => File.WriteAllText(GetPath(fileName), JsonUtility.ToJson(obj));

    protected bool TryLoadObject<T>(string fileName, out T obj) 
        => TryGetObject(File.Exists(GetPath(fileName)), fileName, LoadFromJsonUtility<T>, out obj);

    private bool TryGetObject<T>(bool condition, string key, LoadObject<T> loadObject, out T result)
    {
        result = default;

        if (condition)
            result = loadObject(key);

        return condition != false;
    }

    private T LoadFromJsonUtility<T>(string fileName) 
        => JsonUtility.FromJson<T>(File.ReadAllText(GetPath(fileName)));

    private int LoadFromPlayerPrefs(string key) 
        => PlayerPrefs.GetInt(key);

    private string GetPath(string fileName)
        => Path.Combine(Application.dataPath, fileName);
}