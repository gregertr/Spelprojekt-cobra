using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public string LevelName;
    public AudioClip[] DoorSounds;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player" && Input.GetKeyUp("e"))
        {
            SoundManager.Instance.RandomizeSFX(DoorSounds);
            SceneManager.LoadScene(LevelName);
        }
    }
}
