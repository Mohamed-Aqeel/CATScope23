using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EternalFlame : MonoBehaviour
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
    private enum PlayerAnimator { Idle, Running, Jumping, Falling, Entering, ShortAttack }
    private PlayerAnimator currentState;

    // متغيرات الهجوم والصوت
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private int shortAttackDamage = 10;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip shortAttackSound;

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

        // الهجوم القصير
        if (Input.GetKeyDown(KeyCode.Z))
        {
            currentState = PlayerAnimator.ShortAttack;
            audioSource.PlayOneShot(shortAttackSound);
            ShortAttack(); // استدعاء دالة الهجوم القصير
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

    // دالة الضرب القصير
    private void ShortAttack()
    {
        // الكشف عن الأعداء في نطاق الهجوم
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);

        // إيجاد الأعداء وتحديث الضرر الخاص بهم
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
/*                enemy.GetComponent<Enemy>().TakeDamage(shortAttackDamage);
*/            }
        }
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
}