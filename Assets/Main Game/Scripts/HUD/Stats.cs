using TMPro;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public TextMeshProUGUI CriticalChance;
    public TextMeshProUGUI MeleeDmg;

    private Player player;
    private Punch punchScript;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        punchScript = player.GetComponent<Punch>();
    }

    // Update is called once per frame
    void Update()
    {
        CriticalChance.text = $"Critchance {player.Critchance}%";
        MeleeDmg.text = $"Melee Dmg {punchScript.Damage}";
    }
}
