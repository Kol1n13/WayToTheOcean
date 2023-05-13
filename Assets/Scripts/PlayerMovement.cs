using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anime;
    private SpriteRenderer sprite;
    private BoxCollider2D coll;

    private Transform gunPoint;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private LayerMask jumpableGroud;

    private enum MovementState { idle, running, jumping, falling }

    [SerializeField] private AudioSource jumpSound;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
        gunPoint = transform.Find("GunPoint");
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jumpSound.Play();
            rb.velocity = new Vector3(rb.velocity.x, jumpForce);
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        MovementState state;
        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
            gunPoint.localPosition = new Vector3(1.2f, gunPoint.localPosition.y, gunPoint.localPosition.z);
            gunPoint.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
            gunPoint.localPosition = new Vector3(-1.2f, gunPoint.localPosition.y, gunPoint.localPosition.z);
            gunPoint.rotation = Quaternion.Euler(0f, 0f, 180f);
        }
        else
        {
            state = MovementState.idle;
        }
        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        if (rb.velocity.y < -.1f && Player_Life.isOnFan == false)
        {
            state = MovementState.falling;
        }
        anime.SetInteger("MovementState", (int)state);
    }



    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGroud);
    }
}