using System.Collections;
using System.Linq;
using UnityEngine;

public class Punch : MonoBehaviour
{
    public Animator Animator;
    public GameObject PunchEffect;
    public Transform MeleePoint;
    public float AttackRange;
    public int Damage = 50;

    [Range(0.1f, 2.5f)]
    public float WindupDelay = 1f;

    [HideInInspector]
    public bool IsPunching;

    public void DoPunch(AudioClip[] clips)
    {
        Animator.SetBool("canTransition", false);
        IsPunching = true;
        Animator.SetTrigger("attack2");
        var enemiesHit = Physics2D.OverlapCircleAll(transform.position, AttackRange).Where(x => x.GetComponent<Enemy>() != null);

        foreach (var enemies in enemiesHit)
        {
            var enemy = enemies.GetComponent<Enemy>();

            enemy.TakeDamage(Damage, false);
            enemy.BounceBack(2);
        }

        StartCoroutine(DoEffect(clips));
        StartCoroutine(PunchDelay());
    }

    IEnumerator DoEffect(AudioClip[] clips)
    {
        yield return new WaitForSeconds(0.15f);

        SoundManager.Instance.RandomizeSFX(clips);

        if (PunchEffect != null)
        {
            var player = GameObject.FindGameObjectWithTag("Player").transform;
            var isRight = player.position.x < transform.position.x;
            var originalRot = PunchEffect.transform.rotation;
            var rotation = isRight ? originalRot : Quaternion.Euler(-originalRot.eulerAngles);
            var from = MeleePoint == null ? transform.position : MeleePoint.position;
            var obj = Instantiate(PunchEffect, from, rotation);
            Destroy(obj, 1);
        }
    }

    IEnumerator PunchDelay()
    {
       yield return new WaitForSeconds(WindupDelay);
        IsPunching = false;
        Animator.SetBool("canTransition", true);
    }
}