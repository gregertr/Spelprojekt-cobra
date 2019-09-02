using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    [HideInInspector]
    public bool IsGameOver;

    public Fire FireScript;
    
    public AudioClip[] GameOverSounds;
    public AudioClip[] PlayerHurtSounds;
    public AudioClip[] PunchSounds;
    public AudioClip[] FireShotSounds;

    private Player playerComponent;
    private GameObject playerObj;
    private Punch PunchScript;

    void Awake()
    {
        playerObj = GameObject.FindWithTag("Player");
        playerComponent = playerObj.GetComponent<Player>();
        PunchScript = playerComponent.GetComponentInChildren<Punch>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (IsGameOver)
        {
            SceneManager.LoadScene("GameOver");
            SceneManager.UnloadSceneAsync("Demo");

            //SoundManager.Instance.RandomizeSFX(GameOverSounds);
            //SoundManager.Instance.Music.Stop();
        }

        if (Input.GetButtonDown("Fire1") && !PunchScript.IsPunching)
        {
            PunchScript.DoPunch(PunchSounds);
        }

        // Fire 
        if (Input.GetButtonDown("Fire2"))
        {
            if (FireScript.FireShot())
            {
                SoundManager.Instance.RandomizeSFX(FireShotSounds);
            }
        }
    }
}
