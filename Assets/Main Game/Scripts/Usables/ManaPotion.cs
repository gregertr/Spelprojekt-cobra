using UnityEngine;

public class ManaPotion : MonoBehaviour
{
    public int Amount;
    public int Strength;

    private Player player;
    private Fire magic;
    //public SimpleHealthBar ManaBar;

    // Start is called before the first frame update
    void Start()
    {
        var playerObj = GameObject.FindWithTag("Player");
        player = playerObj.GetComponent<Player>();
        magic = playerObj.GetComponent<Fire>();
    }

    public bool Consume()
    {
        if (Amount <= 0 || player.Mana >= 100)
            return false;

        Amount--;

        player.Mana += Strength;
        player.ManaBar.UpdateBar(player.Mana, player.MaxMana);
        gameObject.SetActive(false);

        return true;
    }
}
