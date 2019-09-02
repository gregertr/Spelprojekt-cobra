using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Deathscreen : MonoBehaviour
{
    public Text text;
    [Range(0f, 10)]
    public float fadeTime;
    // Start is called before the first frame update
    void Start()
    {
        text.CrossFadeAlpha(0, 0f, true);
        text.CrossFadeAlpha(1, fadeTime, false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Level 1");
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }

    }
}
