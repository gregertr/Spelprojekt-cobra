using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelBoundary : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
