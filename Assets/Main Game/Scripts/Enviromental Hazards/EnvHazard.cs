using UnityEngine;

public class EnvHazard : MonoBehaviour
{
    public AudioClip[] Clips;

    public float DamagePerSecond = 0.15f;

    private Player player;
    private bool active;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag != "Player")
            return;

        if (active == false)
        {
            active = true;
            EnvHazardAttack();
        }
        active = false;
    }
    public void EnvHazardAttack()
    {
        player.Health = player.Health - DamagePerSecond;
        player.HealthBar.UpdateBar(player.Health, player.MaxHealth);
        SoundManager.Instance.RandomizeSFX(Clips);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        SoundManager.Instance.SFX.Stop();
    }
}
