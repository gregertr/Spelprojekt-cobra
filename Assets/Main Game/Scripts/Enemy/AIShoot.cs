using UnityEngine;

public class AIShoot : MonoBehaviour
{
    public GameObject Projectile;
    public Transform FirePoint;

    public float FireRate = 2500;
    private float fireDelay;

    private Enemy enemy;
    private Player player;

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        enemy = gameObject.GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!enemy.IsAggro || enemy.IsDying || Vector2.Distance(player.transform.position, enemy.transform.position) > 15)
        {
            return;
        }

        if (Time.time > fireDelay)
        {
            // ranged attack
            Instantiate(Projectile, FirePoint.position, Quaternion.identity);
            fireDelay = Time.time + FireRate / 1000;
        }
    }
}
