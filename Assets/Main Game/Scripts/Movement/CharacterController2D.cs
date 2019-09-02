using System;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    // Amount of force added when the player jumps.
    [SerializeField]
    private float JumpForce = 500f;

    public GameObject JumpEffect;

    public GameObject LandEffect;

    public AudioClip[] JumpSounds;
    public AudioClip[] LandClips;

    private Player player;

    // How much to smooth out the movement
    [Range(0, .3f)]
    [SerializeField]
    private float MovementSmoothing = .05f; 

    // Whether or not a player can steer while jumping;     
    [SerializeField]
    private bool AirControl = true;                   


    [SerializeField]
    public Transform GroundCheck;

    // Radius of the overlap circle to determine if grounded
    const float GroundedRadius = .2f;

    // Whether or not the player is grounded.
    [HideInInspector]
    public bool Grounded;

    private Rigidbody2D rigidbody;
    private bool facingRight = true;  
    private Vector3 velocity;

    private int numberOfJumps;
    private readonly int maxJumpAmount = 1;

    [Header("Events")]
    [Space]
    public UnityEvent OnLandEvent;

    [Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        var playerObj = GameObject.FindGameObjectWithTag("Player");
        player = playerObj.GetComponent<Player>();

        if (OnLandEvent == null)
        {
            OnLandEvent = new UnityEvent();
        }
    }

    public void OnLand()
    {
        if (LandClips != null && SoundManager.Instance != null)
        SoundManager.Instance.RandomizeSFX(LandClips);
    }

    void FixedUpdate()
    {
        var wasGrounded = Grounded;
        Grounded = false;

        var colliders = Physics2D.OverlapCircleAll(GroundCheck.position, GroundedRadius);
        foreach (var t in colliders)
        {
            if (t.gameObject == gameObject || t.tag == "IgnoreCollision")
            {
                continue;
            }

            Grounded = true;
            numberOfJumps = 0;

            if (wasGrounded)
            {
                continue;
            }

            if (t.tag == "Enemy")
            {
                player.BounceUp();
                var enemy = t.GetComponent<Enemy>();
                enemy.TakeDamage(25);
            }

            OnLandEvent.Invoke();
        }
    }

    public void Move(float move, bool jump, bool doublejump)
    {
        if (Grounded || AirControl)
        {
            Vector3 targetVelocity = new Vector2(move * 10f, rigidbody.velocity.y);

            rigidbody.velocity = Vector3.SmoothDamp(rigidbody.velocity, targetVelocity, ref velocity, MovementSmoothing);

            if (move > 0 && !facingRight || move < 0 && facingRight)
            {
                Flip();
            }
        }

        if (!jump)
        {
            return;
        }

        if (Grounded)
        {
            Jump();

            if (JumpEffect != null)
            {
                var pos = GroundCheck;
                var obj = Instantiate(JumpEffect, pos.position, Quaternion.identity);
                Destroy(obj, 2);
            }
        }
        else if (doublejump && numberOfJumps < maxJumpAmount)
        {
            numberOfJumps++;
            Jump();
        }
    }

    private void Jump()
    {
        Grounded = false;
        rigidbody.AddForce(new Vector2(0f, JumpForce));
        SoundManager.Instance.RandomizeSFX(JumpSounds);
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}