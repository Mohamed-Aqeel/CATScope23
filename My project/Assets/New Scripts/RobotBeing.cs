using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotBeing : MonoBehaviour
{

    // متغيرات الحركة والقفز
    [SerializeField] private float moveSpeed = 5f;
    private bool isGrounded;
    private float movement;

    // المتغيرات اللازمة لتحريك اللاعب
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sprite;

    // متغيرات الأنيميشن
    private enum PlayerAnimator { Idle, Running, ShortAttack, LongAttack, Entering }
    private PlayerAnimator currentState;

    // متغيرات الهجوم
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private int shortAttackDamage = 10;
    [SerializeField] private GameObject shotPrefab;
    [SerializeField] private Transform shotSpawn;
    [SerializeField] private float shotSpeed = 10f;
    [SerializeField] private float shotLifetime = 2f;

    // متغيرات الصوت
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip shortAttackSound;
    [SerializeField] private AudioClip longAttackSound;

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
        if (movement != 0)
        {
            currentState = PlayerAnimator.Running;
        }
        else
        {
            currentState = PlayerAnimator.Idle;
        }

        // الهجوم القصير
        if (Input.GetKeyDown(KeyCode.Z))
        {
            currentState = PlayerAnimator.ShortAttack;
            audioSource.PlayOneShot(shortAttackSound);
        }

        // الهجوم الطويل
        if (Input.GetKeyDown(KeyCode.X))
        {
            currentState = PlayerAnimator.LongAttack;
            audioSource.PlayOneShot(longAttackSound);
            LongAttack(); // استدعاء دالة الهجوم الطويل
        }

        // التحديث للأنيميشن
        UpdateAnimator();
    }

    // دالة التحديث للأنيميشن
    private void UpdateAnimator()
    {
        animator.SetInteger("State", (int)currentState);
        if (movement < 0)
        {
            sprite.flipX = true;
        }
        else if (movement > 0)
        {
            sprite.flipX = false;
        }
    }

    // دالة الضرب
    private void ShortAttack()
    {
        // الكشف عن الأعداء في نطاق الهجوم
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);

        // إطلاق الصوت
        audioSource.PlayOneShot(shortAttackSound);

        // إيجاد الأعداء وتحديث الضرر الخاص بهم
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
/*                enemy.GetComponent<Enemy>().TakeDamage(shortAttackDamage);
*/            }
        }
    }

    // دالة الهجوم الطويل
    private void LongAttack()
    {
        // إنشاء طلقة جديدة
        GameObject shot = Instantiate(shotPrefab, shotSpawn.position, Quaternion.identity);

        // تحديد سرعة الطلقة
        Rigidbody2D shotRigidbody2D = shot.GetComponent<Rigidbody2D>();
        Vector2 shotDirection = sprite.flipX ? Vector2.left : Vector2.right;
        shotRigidbody2D.velocity = shotDirection * shotSpeed;

        // تحديد موقع الطلقة
        Vector3 shotSpawnPosition = shotSpawn.position;
        float spawnOffsetX = sprite.flipX ? -0.3f : 0.3f;
        float spawnOffsetY = -0.05f;
        shot.transform.position = new Vector3(shotSpawnPosition.x + spawnOffsetX, shotSpawnPosition.y + spawnOffsetY, shotSpawnPosition.z);

        // تحديد تدوير الطلقة
        Vector3 shotRotation = shot.transform.rotation.eulerAngles;
        shotRotation.y = sprite.flipX ? 180.0f : 0.0f;
        shot.transform.rotation = Quaternion.Euler(shotRotation);

        // إتلاف الطلقة بعد مدة معينة إذا لم تصطدم بأي عدو
        Destroy(shot, shotLifetime);
    }

    // دالة الانتهاء من الدخول
    private void FinishEntering()
    {
        rb.isKinematic = false;
        GetComponent<BoxCollider2D>().isTrigger = false;
        currentState = PlayerAnimator.Idle;
    }

    // الاختبار إذا كان اللاعب قد وصل إلى الأرض
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    // الاختبار إذا توقف عن اللمس بالأرض
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    // دالة الرسم لنطاق الهجوم
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
