using System.Collections;
using System.Linq;
using UnityEngine;

public class AIPunch : MonoBehaviour
{
    public Animator Animator;
    public GameObject PunchEffect;
    public Transform MeleePoint;
    public float AttackRange;
    public int Damage = 15;

    [Range(0.1f, 1.5f)]
    public float WindupDelay = 0.15f;

    [Range(0.1f, 1.5f)]
    public float AnimationDelay = 1f;

    [Range(1f, 150)]
    public float Force = 50;

    public float PunchRate = 1750;
    private float punchDelay;

    private Enemy enemy;

    private Player playerObj;

    [HideInInspector]
    public bool IsPunching;

    void Awake()
    {
        enemy = gameObject.GetComponent<Enemy>();
        playerObj = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        if (!enemy.IsAggro || enemy.IsDying)
        {
            return;
        }

        if (Time.time > punchDelay && Vector2.Distance(transform.position, playerObj.transform.position) <= AttackRange)
        {
            // melee attack
            DoPunch();
            punchDelay = Time.time + PunchRate / 1000;
        }
    }

    public void DoPunch()
    {
        Animator.SetBool("canTransition", false);
        IsPunching = true;
        Animator.SetTrigger("attack1");

        StartCoroutine(AnimationStopDelay());
        StartCoroutine(PunchDelay());
    }

    IEnumerator AnimationStopDelay()
    {
        yield return new WaitForSeconds(AnimationDelay);
        IsPunching = false;
        Animator.SetBool("canTransition", true);
    }

    IEnumerator PunchDelay()
    {
        yield return new WaitForSeconds(WindupDelay);

        if (PunchEffect != null)
        {
            var from = MeleePoint == null ? transform.position : MeleePoint.position;
            var obj = Instantiate(PunchEffect, from, Quaternion.identity);
            Destroy(obj, 1);
        }

        if (Physics2D.OverlapCircleAll(transform.position, AttackRange).Any(x => x.tag == "Player"))
        {
            if (enemy.IsBoss)
            {
                playerObj.Flatten();
            }
            playerObj.TakeDamage(Damage + enemy.Level);
            playerObj.BounceBack(Force, transform.position.x < playerObj.transform.position.x);
        }
    }
}
