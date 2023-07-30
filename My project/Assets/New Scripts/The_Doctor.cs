using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class The_Doctor:MonoBehaviour
{
    // متغيرات الحركة والقفز
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    private bool isGrounded;
    private float movement;

    // المتغيرات اللازمة لتحريك اللاعب
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sprite;

    // متغيرات الأنيميشن
    private enum PlayerAnimator { Idle, Running, Jumping, Falling, Entering }
    private PlayerAnimator currentState;

    // متغير الصوت
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip jumpSound;

    // دالة البدء
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        currentState = PlayerAnimator.Idle;
    }

    // دالة التحديث
    private void Update()
    {
        // الحركة
        movement = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(movement * moveSpeed, rb.velocity.y);

        // الانتقال بين الحالات
        if (isGrounded)
        {
            if (movement == 0)
            {
                currentState = PlayerAnimator.Idle;
            }
            else
            {
                currentState = PlayerAnimator.Running;
            }
        }
        else
        {
            if (rb.velocity.y > 0)
            {
                currentState = PlayerAnimator.Jumping;
            }
            else if (rb.velocity.y < 0)
            {
                currentState = PlayerAnimator.Falling;
            }
        }

        // القفز
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            audioSource.PlayOneShot(jumpSound);
        }

        // التحديث للأنيميشن
        UpdateAnimator();
    }

    // دالة التحديث للأنيميشن
    private void UpdateAnimator()
    {
        animator.SetInteger("State", (int)currentState);
        sprite.flipX = movement < 0;
    }

    // دالة الانتهاء من الدخول
    private void FinishEntering()
    {
        rb.isKinematic = false;
        GetComponent<BoxCollider2D>().isTrigger = false;
        currentState = PlayerAnimator.Idle;
    }

    // الاختبار إذا كان اللاعب يتلامس بالأرض
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    // الاختبار إذا انفصل اللاعب عن الأرض
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    // الاختبار إذا دخل اللاعب في المنطقة المحددة
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enter"))
        {
            rb.isKinematic = true;
            GetComponent<BoxCollider2D>().isTrigger = true;
            currentState = PlayerAnimator.Entering;
        }
    }
}
