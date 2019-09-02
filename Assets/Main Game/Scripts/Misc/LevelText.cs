using TMPro;
using UnityEngine;

public class LevelText : MonoBehaviour
{
    public TextMeshProUGUI TextMeshPro;
    public Enemy EnemyScript;

    private Quaternion rotation;

    void Awake()
    {
        rotation = transform.rotation;
    }

    void LateUpdate()
    {
        TextMeshPro.text = $"Lvl {EnemyScript.Level}";
        TextMeshPro.transform.rotation = rotation;
    }
}
