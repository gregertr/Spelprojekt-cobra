using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    public int Amount;
    public int Strength;

    //public SimpleHealthBar HealthBar;

    private Player player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public bool Consume()
    {
        if (Amount <= 0 || player.Health >= 100)
            return false;

        Amount--;
        player.Health += Strength;
        player.HealthBar.UpdateBar(player.Health, player.MaxHealth);
        gameObject.SetActive(false);

        return true;
    }
}
