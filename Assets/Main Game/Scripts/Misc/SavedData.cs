using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavedData : MonoBehaviour
{
    private Stopwatch totalTime;
    public static float Time;
    public static int EnemyKilled;

    public string Date { get; set; }
    public string Name { get; set; }
    public string Score { get; set; }

    public string GetFullString()
    {
        return Date == "" ? "" : $"{Score} {Name} {Date}";
    }

    // Start is called before the first frame update
    void Start()
    {
        totalTime = new Stopwatch();
        totalTime.Start();

        SceneManager.activeSceneChanged += OnSceneChange;
    }

    private void OnSceneChange(Scene arg0, Scene arg1)
    {
        totalTime.Stop();
        Time = totalTime.Elapsed.Seconds;
    }
}
