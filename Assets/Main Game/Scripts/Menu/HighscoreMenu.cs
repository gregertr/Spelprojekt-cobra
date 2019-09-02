using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HighscoreMenu : MonoBehaviour
{
    private List<SavedData> texts;
    public GameObject TextObj1;
    public GameObject TextObj2;
    public GameObject TextObj3;

    // Start is called before the first frame update
    void Start()
    {
        texts = FileManager.Read();

        TextObj1.GetComponentInChildren<TextMeshProUGUI>().text = texts[0].GetFullString();
        TextObj2.GetComponentInChildren<TextMeshProUGUI>().text = texts[1].GetFullString();
        TextObj3.GetComponentInChildren<TextMeshProUGUI>().text = texts[2].GetFullString();
    }
}
