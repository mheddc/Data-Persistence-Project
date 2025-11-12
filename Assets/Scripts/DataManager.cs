using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public string PlayerName;

    // Laufzeit: bequem als Dictionary
    public Dictionary<string, int> values = new Dictionary<string, int>();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadScore();
    }

    public int? Get(string name)
    {
        if (values == null)
        {
            Debug.Log("NULLNULL"); // sollte mit diesem Fix nicht mehr auftreten
            return null;
        }

        if (values.TryGetValue(name, out int result))
            return result;

        return null;
    }

    public void Set(string name, int value)
    {
        if (values == null) values = new Dictionary<string, int>();
        values[name] = value;
    }

    public void SaveScore()
    {
        // Dictionary -> serialisierbare Liste umwandeln
        var data = new NameValueSet();
        foreach (var kv in values)
        {
            data.items.Add(new NameIntPair { name = kv.Key, value = kv.Value });
        }

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Path.Combine(Application.persistentDataPath, "savefile2.json"), json);
    }

    public void LoadScore()
    {
        string path = Path.Combine(Application.persistentDataPath, "savefile2.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            var data = JsonUtility.FromJson<NameValueSet>(json);

            // Fallback falls Datei leer/defekt
            values = new Dictionary<string, int>();
            if (data != null && data.items != null)
            {
                foreach (var p in data.items)
                {
                    if (!string.IsNullOrEmpty(p.name))
                        values[p.name] = p.value;
                }
            }
            Debug.Log("Load ok");
        }
        else
        {
            Debug.Log("No save, init new");
            values = new Dictionary<string, int>();
        }
    }
}

[Serializable]
public class NameValueSet
{
    // JsonUtility kann Listen von [Serializable]-Typen
    public List<NameIntPair> items = new List<NameIntPair>();
}

[Serializable]
public class NameIntPair
{
    public string name;
    public int value;
}
