using UnityEngine;

public class AIProjectile : MonoBehaviour
{
    [Range(5, 40)]
    public int Speed = 30;

    [Range(5, 40)]
    public int Damage = 10;

    public Rigidbody2D Rigidbody2D;
    public GameObject ImpactEffect;
    private Vector2 target;

    private GameObject playerObj;
    private Player playerComponent;

    void Awake()
    {
        playerObj = GameObject.FindWithTag("Player");
        playerComponent = playerObj.GetComponent<Player>();
    }

    // Start is called before the first frame update
    void Start()
    {
        var random = Random.Range(1.5f, 3.5f);
        var from = transform.position + new Vector3(0, -random, 0);
        var direction = playerComponent.transform.position - from;
        direction.Normalize();
        Rigidbody2D.velocity = direction * Speed;
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Enemy" || target.tag == "IgnoreCollision" || playerComponent.IsDying)
        {
            return;
        }

        if (target.tag == "Player")
        {
            playerComponent.TakeDamage(Damage);
            playerComponent.BounceBack(20, target.transform.position.x > transform.transform.position.x);
        }

        if (ImpactEffect != null)
        {
            var obj = Instantiate(ImpactEffect, transform.position, Quaternion.identity);
            Destroy(obj, 1);
        }

        Destroy(gameObject);
    }
}
