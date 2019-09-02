using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D Controller;
    private Animator Animator;

    public AudioClip WalkSound;

    public float Speed = 40f;
    private float horizontalM;
    private bool jump;
    private bool isRunning;

    [HideInInspector]
    public bool Doublejump;

    void Start()
    {
        Animator = gameObject.GetComponent<Animator>();
        Animator.SetTrigger("idle");
    }

    // Update is called once per frame
    void Update()
    {
        horizontalM = Input.GetAxisRaw("Horizontal") * Speed;
        Animator.SetFloat("Speed", Mathf.Abs(horizontalM));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            Animator.SetBool("jumping", true);
        }

        if (!isRunning && horizontalM != 0 && Animator.GetBool("canTransition"))
        {
            isRunning = true;
            Animator.SetTrigger("run");
        }

        if (Animator.GetCurrentAnimatorStateInfo(0).IsName("run") && !Animator.GetBool("jumping"))
        {
           //SoundManager.Instance.RandomizeSFX(WalkSound);
        }

        if (isRunning && horizontalM == 0 && Animator.GetBool("canTransition"))
        {
            isRunning = false;
            Animator.SetTrigger("idle");
        }
    }

    void LateUpdate()
    {
        Controller.Move(horizontalM * Time.fixedDeltaTime, jump, Doublejump);
        jump = false;
    }
}
