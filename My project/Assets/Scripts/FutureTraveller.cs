﻿using UnityEngine;

public class FutureTraveller : MonoBehaviour
{
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private float movementSpeed = 7f;
    private Rigidbody2D rb;
    private Animator anime;
    private float movement;
    private SpriteRenderer sprite;
    private enum PlayerAnimator { Player_Stop, Player_Running, Player_Jumping, Player_Falling }
    private BoxCollider2D collid;
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource fallSound;
    private PlayerAnimator state;
    [SerializeField] private string groundLayerName = "Ground";
    private LayerMask groundLayerMask;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        collid = GetComponent<BoxCollider2D>();
        groundLayerMask = LayerMask.GetMask(groundLayerName);

        // تعيين صوت السير إلى الخاصية clip الخاصة بالـ AudioSource
        jumpSound.clip = Resources.Load<AudioClip>("WalkSound");
        // تعيين صوت السقوط إلى الخاصية clip الخاصة بالـ AudioSource
        fallSound.clip = Resources.Load<AudioClip>("FallSound");
    }

    private void Update()
    {
        movement = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(movement * movementSpeed, rb.velocity.y);

        // تشغيل صوت السير عندما يتحرك اللاعب
        if (movement != 0 && IsOnGround() && !jumpSound.isPlaying)
        {
            jumpSound.Play();
        }

        if (Input.GetButtonDown("Jump") && IsOnGround())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpSound.Play();
        }

        Animator();
    }

    private bool IsOnGround()
    {
        RaycastHit2D hit = Physics2D.BoxCast(collid.bounds.center, collid.bounds.size, 0f, Vector2.down, .1f, groundLayerMask);
        return hit.collider != null;
    }

    private void Animator()
    {
        if (movement > 0f)
        {
            state = PlayerAnimator.Player_Running;
            sprite.flipX = false;
        }
        else if (movement < 0f)
        {
            state = PlayerAnimator.Player_Running;
            sprite.flipX = true;
        }
        else
        {
            state = PlayerAnimator.Player_Stop;
        }

        if (IsOnGround())
        {
            if (rb.velocity.y < -.1f)
            {
                state = PlayerAnimator.Player_Stop;
                // تشغيل صوت السقوط عندما يسقط اللاعب
                if (!fallSound.isPlaying)
                {
                    fallSound.Play();
                }
            }
        }
        else if (rb.velocity.y > .1f)
        {
            state = PlayerAnimator.Player_Jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = PlayerAnimator.Player_Falling;
        }

        anime.SetInteger("State", (int)state);
    }
}