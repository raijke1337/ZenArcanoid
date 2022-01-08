using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class DebugFunctionsManager : MonoBehaviour
{

    private string path;

    public void AddLogEvent(string record)
    {
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(record);
        writer.Close();
    }

    private void Start()
    {    
        path = Application.dataPath + "EditorLogging.txt";
        CheckLog();
    }

    private void CheckLog()
    {
        if (File.Exists(path))
        {
            Debug.Log($"Writing to {path}");
        }
        else
        {
            Debug.Log("Creating a new log file");
            var txt = File.Create(path);
            txt.Close();
        }
    }


}
