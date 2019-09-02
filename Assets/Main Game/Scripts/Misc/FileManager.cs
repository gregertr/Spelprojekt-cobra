using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

public class FileManager
{
    static FileManager() // static Constructor
    {
        var currentPath = Environment.GetEnvironmentVariable("Main Game", EnvironmentVariableTarget.Process);
#if UNITY_EDITOR_32
        var dllPath = Application.dataPath
        + Path.DirectorySeparatorChar + "Main Game"
        + Path.DirectorySeparatorChar + "Plugins"
        + Path.DirectorySeparatorChar + "x86";
#elif UNITY_EDITOR_64
        var dllPath = Application.dataPath
                      + Path.DirectorySeparatorChar + "Main Game"
                      + Path.DirectorySeparatorChar + "Plugins"
                      + Path.DirectorySeparatorChar + "x86_64";
#else // Player
        var dllPath = Application.dataPath + Path.DirectorySeparatorChar + "Plugins";

#endif

        if (currentPath != null && currentPath.Contains(dllPath) == false)
        {
            Environment.SetEnvironmentVariable("Main Game", $"{currentPath}{Path.PathSeparator}{dllPath}", EnvironmentVariableTarget.Process);
        }
    }

    [DllImport("Highscore")]
    public static extern float Save(string path, int score, string name, string date);

    public static List<SavedData> Read()
    {
        var dllPath = Application.dataPath + "/Highscore.txt";
        Debug.Log(dllPath);
        var data = new List<SavedData>();

        var reader = new StreamReader(dllPath);
        string line;

        while ((line = reader.ReadLine()) != null)
        {
            var newLine = line.Split(',');
            
            var date = newLine[0];
            var name = newLine[1];
            var score = newLine[2];

            data.Add(new SavedData
            {
                Date = date,
                Name = name,
                Score = score
            });
        }

        data = data.OrderByDescending(x => int.Parse(x.Score)).ToList();
        reader.Close();

        return data;
    }
}
