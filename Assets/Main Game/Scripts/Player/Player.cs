using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float Health = 100;
    public float Mana = 100;
    public int Level = 1;
    
    [HideInInspector]
    public float Critchance = 5;

    [HideInInspector]
    public float Experience;
    private float experienceLeft = 100;

    public SimpleHealthBar HealthBar;
    public SimpleHealthBar ManaBar;
    public SimpleHealthBar ExpBar;

    [HideInInspector]
    public float MaxHealth;

    [HideInInspector]
    public float MaxMana;

    [HideInInspector]
    public bool IsDying;

    [HideInInspector]
    public bool IsFlatten;

    [HideInInspector]
    public Animator Animator;

    [HideInInspector]
    public Rigidbody2D Rigidbody;

    void Awake()
    {
        Rigidbody = gameObject.GetComponent<Rigidbody2D>();
        Animator = gameObject.GetComponent<Animator>();
    }

    void Start()
    {
        MaxHealth = Health;
        MaxMana = Mana;
        SetExp(0, 100);
    }

    void Update()
    {
        if (transform.position.y <= -30 || Health <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    public void TakeDamage(int damage, bool hurt1 = true)
    {
        CameraShake.Instance.Shake();
        RemoveHealth(damage);

        if (Health <= 0 && !IsDying)
        {
            IsDying = true;
            Animator.SetTrigger("dead");
        }
        else if (Animator.GetBool("canTransition"))
        {
            Animator.SetTrigger(hurt1 ? "hurt1" : "hurt2");
        }
    }

    public void BounceBack(float forceX, bool isRight)
    {
        if (IsDying)
        {
            return;
        }

        forceX = isRight ? forceX : -forceX;
        var velocity = new Vector2(forceX, 0);

        Rigidbody.AddForce(velocity, ForceMode2D.Impulse);
    }

    public void BounceUp()
    {
        Rigidbody.AddForce(new Vector2(0, 35), ForceMode2D.Impulse);
    }

    public void Flatten()
    {
        CameraShake.Instance.Shake();
        transform.localScale = new Vector3(0.01f, 0.0015f, 0.01f);
        StartCoroutine(FlattenDelay());
    }

    IEnumerator FlattenDelay()
    {
        yield return new WaitForSeconds(2);
        transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
    }

    public bool IsCrit()
    {
        return Random.Range(0, 100) <= Critchance;
    }

    public void RemoveMana(float amount)
    {
        Mana -= amount;

        if (Mana > 100)
            MaxMana = Mana;
        else
            MaxMana = 100;

        ManaBar.UpdateBar(Mana, MaxMana);
    }

    public void AddMana(float amount)
    {
        Mana += amount;
        MaxMana = Mana > MaxMana ? Mana : MaxMana;
        ManaBar.UpdateBar(Mana, MaxMana);
    }

    public void RefreshMana()
    {
        Mana = MaxMana;
        ManaBar.UpdateBar(Mana, MaxMana);
    }

    public void RemoveHealth(float amount)
    {
        Health -= amount;

        if (Health > 100)
            MaxHealth = Health;
        else
            MaxHealth = 100;

        HealthBar.UpdateBar(Health, MaxHealth);
    }

    public void AddHealth(float amount)
    {
        Health += amount;
        MaxHealth = Health > MaxHealth ? Health : MaxHealth;
        HealthBar.UpdateBar(Health, MaxHealth);
    }

    public void RefreshHealth()
    {
        Health = MaxHealth;
        HealthBar.UpdateBar(Health, MaxHealth);
    }

    public void SetExp(float amount, float max)
    {
        Experience = amount;
        ExpBar.UpdateBar(Experience, max);
    }

    public void AddExp(float amount)
    {
        Experience += amount;
        if (Experience >= experienceLeft)
        {
            OnLevelUp();
        }
        else
        {
            ExpBar.UpdateBar(Experience, experienceLeft);
        }
    }
    private void Death()
    {
        SceneManager.LoadScene("GameOver");
    }

    private void OnLevelUp()
    {
        Level++;

        Critchance = Mathf.Min(100, 15 * Level * 0.6f);
        RefreshHealth();
        RefreshMana();

        Experience -= experienceLeft;
        var t = Mathf.Pow(1.15f, Level);
        experienceLeft = (int)Mathf.Floor(100 * t);

        GameObject.Find("LevelText").GetComponent<Text>().text = $"{Level}";
        ExpBar.UpdateBar(Experience, experienceLeft);
    }
}
