using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public ParticleSystem dust;

    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashTime = 1f;
     private float currentDashTime;
    private bool isDashing;

    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer Sprite;

    private Animator anim;

    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    [SerializeField] private AudioSource jumpSoundEffect;




    private enum MovementState { idle, running, jumping, falling, dashing }


    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        Sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            CreateDust();
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing)
        {
            currentDashTime = dashTime;
            isDashing = true;
            anim.SetBool("isDashing", true);
        }
        if (isDashing)
        {
            if (currentDashTime > 0)
            {
                rb.velocity = new Vector2(dirX * dashSpeed, rb.velocity.y);
                currentDashTime -= Time.deltaTime;
            }
            else
            {
                isDashing = false;
                anim.SetBool("isDashing", false);
            }
        }


        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.running;
            Sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            Sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    private void Dash()
    {
        Vector2 dashVelocity = new Vector2(dirX * moveSpeed * 2, 0);
        rb.velocity = Vector2.Lerp(rb.velocity, dashVelocity, Time.deltaTime * 10);
    }
    void CreateDust()
    {
        dust.Play();
    }


}