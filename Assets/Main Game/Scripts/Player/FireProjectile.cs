using UnityEngine;

public class FireProjectile : MonoBehaviour
{
    public float Speed = 30;
    public int Damage = 50;

    public Rigidbody2D Rigidbody2D;
    public GameObject ImpactEffect;

    private Vector2 target;

    void Start()
    {
        var cursor = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var from = transform.position;
        var direction = cursor - from;
        direction.Normalize();
        Rigidbody2D.velocity = direction * Speed;
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Player" || target.tag == "Weapon" || target.tag == "IgnoreCollision")
        {
            return;
        }

        var enemy = target.GetComponent<Enemy>();

        if (enemy != null)
        {
            if (enemy.IsDying)
            {
                return;
            }

            enemy.TakeDamage(Damage);
            enemy.BounceBack(5);
        }

        if (ImpactEffect != null)
        {
            var obj = Instantiate(ImpactEffect, transform.position, Quaternion.identity);
            Destroy(obj, 1);
        }

        Destroy(gameObject);
    }
}