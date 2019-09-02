using UnityEngine;

public class Chest : MonoBehaviour
{
    public Animator Animator;
    public AudioClip[] Clips;
    public GameObject OpenEffect;
    public GameObject NotifyEffect;

    public bool GainDoubleJump; 
    public int GainHealth;
    public int GainMana;
    public float GainSpeed; 
    public int GainDamage; 
    public int GainMagicDamage;
    public int GainMagicVelocity;

    private bool isOpen;
    private GameObject chestSparkleEffect;

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Input.GetKeyDown("e"))
        {
            OpenChest();
        }
    }

    void Start()
    {
        if (NotifyEffect != null && !isOpen)
        {
            chestSparkleEffect = Instantiate(NotifyEffect, transform.position, Quaternion.identity);
        }
    }

    public void OpenChest()
    {
        if (isOpen)
        {
            return;
        }
        isOpen = true;

        if (OpenEffect != null)
        {
            var obj = Instantiate(OpenEffect, transform.position, Quaternion.identity);
            Destroy(obj, 1);
            Destroy(chestSparkleEffect);
        }

        // starts the opening animation
        Animator.SetBool("Open", true);

        // avoids audio stacking
        SoundManager.Instance.RandomizeSFX(Clips);

        var playerObj = GameObject.FindWithTag("Player");
        var playerComponent = playerObj.GetComponent<Player>();
        var playerMovement = playerObj.GetComponent<PlayerMovement>();

        var punch = playerObj.GetComponent<Punch>();
        var fire = playerObj.GetComponent<Fire>();
        var projectile = fire.Projectile.GetComponent<FireProjectile>();

        if (!playerMovement.Doublejump)
        playerMovement.Doublejump = GainDoubleJump;

        playerComponent.AddHealth(GainHealth);
        playerComponent.AddMana(GainMana);
        playerMovement.Speed += GainSpeed;
        punch.Damage += GainDamage;
        projectile.Damage += GainMagicDamage;
        projectile.Speed += GainMagicVelocity;
    }
}
