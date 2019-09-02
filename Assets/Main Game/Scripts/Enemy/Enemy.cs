using System.Collections;
using System.Globalization;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector]
    public float MaxHealth;

    public SimpleHealthBar HealthBar;

    public float Health = 100;
    public GameObject DeathEffect;

    public int Level = 1;

    public AudioClip[] HurtClips;

    public AudioClip[] DeathClips;

    public GameObject DamageTextPrefab;

    public Transform TextSpawn;

    [HideInInspector]
    public bool AlwaysChasePlayer;

    [HideInInspector]
    public bool IsBoss;

    [HideInInspector]
    public bool IsDying;

    [HideInInspector]
    public bool IsAggro;

    [HideInInspector]
    public Rigidbody2D Rigidbody;

    private Player player;

    private Color originalColor;

    [HideInInspector]
    public Animator Animator;

    void Awake()
    {
        Rigidbody = gameObject.GetComponent<Rigidbody2D>();
        Animator = gameObject.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        MaxHealth = Health;
    }

    void LateUpdate()
    {
        if (!IsDying && transform.position.y < -30)
        {
            TakeDamage(int.MaxValue);
        }
    }

    public void TakeDamage(float damage, bool hurt1 = true)
    {
        var isCritical = player.IsCrit();
        if (isCritical)
            damage *= 1.25f;

        var expGained = ExperienceOnKill(damage);
        player.AddExp(expGained);
        Health -= damage;

        var damageText = Instantiate(DamageTextPrefab, TextSpawn);
        damageText.SetActive(true);
        damageText.GetComponent<FloatingDamageText>().CreateDamage(((int)damage).ToString(), this, isCritical, IsBoss);
        Destroy(damageText, 0.5f);

        HealthBar.UpdateBar(Health, MaxHealth);

        SoundManager.Instance.RandomizeSFX(HurtClips);

        if (Health <= 0 && !IsDying)
        {
            SavedData.EnemyKilled++;

            var expText = Instantiate(DamageTextPrefab, TextSpawn);
            
            expText.SetActive(true);
            expText.GetComponent<FloatingDamageText>().CreateExperience(((int)expGained).ToString(CultureInfo.InvariantCulture));
            Destroy(expText, 0.5f);

            var deathEffect = Instantiate(DeathEffect, Rigidbody.position + new Vector2(0, 2), Quaternion.identity);
            Destroy(deathEffect, 1);
            IsDying = true;
            Animator.SetTrigger("dead");
            Rigidbody.velocity = Vector2.zero;
            SoundManager.Instance.RandomizeSFX(DeathClips);

            StartCoroutine(DestroyEnemy());
        }
        else if (Animator.GetBool("canTransition"))
        {
            Animator.SetTrigger(hurt1 ? "hurt1" : "hurt2");
        }
    }

    public void BounceBack(float forceX)
    {
        if (IsDying)
        {
            return;
        }

        var isRight = player.transform.position.x < transform.position.x;
        forceX = isRight ? forceX : -forceX;
        var velocity = new Vector2(forceX, 0);

        Rigidbody.AddForce(velocity, ForceMode2D.Impulse); 
    }

    private float ExperienceOnKill(float damage)
    {
        if (Health - damage > 0)
            return 0;

        var exp = 5f / player.Level + 7.5f;
        if (IsBoss)
            exp *= 2.5f;

        return exp;
    }
   
    IEnumerator DestroyEnemy()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
