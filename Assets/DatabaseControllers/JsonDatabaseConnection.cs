using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JsonDatabaseConnection
{
    public static string load(string path) {
        string jsonFilePath = path.Replace(".json", "");
        TextAsset loadedJsonfile = Resources.Load<TextAsset>(jsonFilePath);
        return loadedJsonfile.text;
    }

    public static void save(string file, string data) {
        // string path = file.Replace(".json", "");
        string path = Application.dataPath+"/Resources/" + file + ".json";
        File.WriteAllText(path, data);

        Debug.Log("Saved!");
    }

    public static void clean(string file) {
        string path = Application.dataPath+"/Resources/" + file + ".json";
        File.WriteAllText(path, "");
    }
}
