using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    public float Speed = 2;
    public float MinDistanceBetween = 2;
    public float MaxDistanceBetween = 6;

    [Range(1f, 3f)]
    public float ChaseTime = 2;

    private bool isIdle;
    private bool isChasing;
    private float chaseOrigin;

    public Transform GroundCheck;

    public Transform CeilingCheck;
    public Transform FloorCheck;

    private Vector2 centre;
    private Vector2 temp;
    private bool isRight = true;
    private float lastFlip;
    private Transform player;

    private bool hasJumped;
    private float lastJump;

    private Enemy enemy;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        enemy = gameObject.GetComponent<Enemy>();

        AnimateRunning();
        FacePlayer();

        if (enemy.name == "Knight")
            enemy.AlwaysChasePlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.IsDying || isIdle)
        {
            return;
        }

        if (enemy.AlwaysChasePlayer) // Chase
        {
            AnimateRunning();
            FacePlayer();
            WalkTowardsPlayer();
        }
        else if (Vector2.Distance(GroundCheck.position, player.position) <= MinDistanceBetween * 1.1f) // Distancing from player
        {
            AnimateRunning();
            WalkAwayFromPlayer();
        }
        else if (Vector2.Distance(GroundCheck.position, player.position) <= MaxDistanceBetween)  // Getting aggro
        {
            AnimateRunning();
            FacePlayer();
            WalkTowardsPlayer();
            SetChaseTimer();
        }
        else if (isChasing && Vector2.Distance(GroundCheck.position, player.position) >= MaxDistanceBetween)
        {
            AnimateRunning();
            FacePlayer();
            Chase();
        }
        else
        {
            AnimateRunning();
            Patrol();
        }
    }

    private void SetChaseTimer()
    {
        chaseOrigin = Time.time;
        isChasing = true;
    }

    void WalkTowardsPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, Speed * Time.deltaTime);

        if (!hasJumped)
        {
            var ceiling = new Vector2(CeilingCheck.position.x, CeilingCheck.position.y);
            var sideCheck = Physics2D.RaycastAll(ceiling, ceiling.x > transform.position.x ? Vector2.right : Vector2.left, 0.5f).Where(x => x.collider.tag == "Path");

            if (sideCheck.Any())
            {
                hasJumped = true;
                enemy.Rigidbody.AddForce(new Vector2(0, 1000));
                StartCoroutine(JumpReset());
            }
        }
    }

    void WalkAwayFromPlayer()
    {
        var pos = transform.position + (transform.position - player.position).normalized * 10;
        transform.position = Vector2.MoveTowards(transform.position, pos, Speed * Time.deltaTime);
    }

    void Patrol()
    {
        enemy.IsAggro = false;

        transform.Translate(Vector2.right * Speed * Time.deltaTime);

        // Checks if entity will fall off the edge
        var floor = new Vector2(FloorCheck.position.x, FloorCheck.position.y);
        var ceiling = new Vector2(CeilingCheck.position.x, CeilingCheck.position.y);

        var downCheck = Physics2D.RaycastAll(floor, Vector2.down, 1)
                                 .Where(x => x.collider.tag != "IgnoreCollision");

        var sideCheck = Physics2D.RaycastAll(ceiling, ceiling.x > transform.position.x ? Vector2.right : Vector2.left, 0.33f)
                                 .Where(x => x.collider.tag != "IgnoreCollision" && x.collider.tag != "Enemy");

        if (!downCheck.Any() || sideCheck.Any())
        {
            if (enemy.Animator.GetBool("canTransition"))
            {
                enemy.Animator.SetTrigger("idle");
            }

            enemy.Animator.SetBool("canTransition", false);
            isIdle = true;
            StartCoroutine(FlipDelayed());
        }
    }

    void AnimateRunning()
    {
        if (enemy.Animator.GetBool("canTransition") &&
            enemy.Animator.GetCurrentAnimatorClipInfo(0)[0].clip.name != "run")
        {
            enemy.Animator.SetTrigger("run");
        }
    }

    IEnumerator FlipDelayed()
    {
        yield return new WaitForSeconds(2);
        Flip();

        isIdle = false;
        isRight = !isRight;
        enemy.Animator.SetBool("canTransition", true);
    }

    void FacePlayer()
    {
        enemy.IsAggro = true;

        if (Vector2.Distance(GroundCheck.position, player.position) >
            Vector2.Distance(transform.position, player.position))
        {
            isRight = !isRight;
            Flip();
        }
    }

    void Flip()
    {
        if (Environment.TickCount - lastFlip <= 500)
        {
            return;
        }

        transform.Rotate(0f, 180f, 0f);
        var objs = gameObject.GetComponents<Transform>();
        foreach (var transform1 in objs.Where(x => x != transform))
        {
            transform1.Rotate(0, -180, 0);
        }

        lastFlip = Environment.TickCount;
    }

    void Chase()
    {
        if (Time.time >= chaseOrigin + ChaseTime && isChasing)
        {
            isChasing = false;
        }
        else
        {
            isChasing = true;
            WalkTowardsPlayer();
        }
    }

    IEnumerator JumpReset()
    {
        yield return new WaitForSeconds(2);
        hasJumped = false;
    }
}