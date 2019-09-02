using UnityEngine;

public class AnimationManager : MonoBehaviour {

	public GameObject[] Objects;

	// Use this for initialization
	void Start () {
		Idle();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Attack()
    {
        foreach (var t in Objects)
        {
            var animator = t.GetComponent<Animator> ();
            animator.SetTrigger("attack1");
        }
    }

	public void Run()
    {
        foreach (var t in Objects)
        {
            var animator = t.GetComponent<Animator> ();
            animator.SetTrigger("run");
        }
    }

	public void Hurt()
    {
        foreach (var t in Objects)
        {
            var animator = t.GetComponent<Animator> ();
            animator.SetTrigger(0 == Random.Range(0, 2) ? "hurt1" : "hurt2");
        }
    }

	public void Dead()
    {
        foreach (var t in Objects)
        {
            var animator = t.GetComponent<Animator> ();
            animator.SetTrigger("dead");
        }
    }

	public void Idle()
    {
        foreach (var t in Objects)
        {
            var animator = t.GetComponent<Animator> ();
            animator.SetTrigger("idle");
        }
    }

}
