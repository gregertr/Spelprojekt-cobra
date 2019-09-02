using UnityEngine;
using UnityEngine.SceneManagement;
using Application = UnityEngine.Application;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadStoryMode()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void LoadSurvivalMode()
    {
        SceneManager.LoadScene("Survival");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
