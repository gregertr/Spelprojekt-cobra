using System;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public void GetInput(string text)
    {
        FileManager.Save(Application.dataPath, SavedData.EnemyKilled, text, DateTime.Now.ToLongDateString());
    }
}
