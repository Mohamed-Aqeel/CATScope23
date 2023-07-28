using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpForce = 14f; // قوة القفز
    [SerializeField] private float movementSpeed = 7f; // سرعة الحركة الأفقية

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sprite;
    private BoxCollider2D collid;

    private enum PlayerState { Idle, Running, Jumping, Falling } // حالات اللاعب

    private PlayerState state;

    [SerializeField] private LayerMask jumpingLayer; // اللير المستخدمة للكشف عن الأرض
    [SerializeField] private AudioSource jumpSound; // مصدر الصوت المستخدم للقفز

    [SerializeField] private GameObject plane; // الطائرة الخاصة باللاعب

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        collid = GetComponent<BoxCollider2D>();
    }

    // تحديث حركة اللاعب
    private void Update()
    {
        float movement = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(movement * movementSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsOnGround())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpSound.Play();
        }

        UpdateAnimator();
    }

    // تحديث حالة اللاعب في الأنيميشن
    private void UpdateAnimator()
    {
        if (Mathf.Abs(rb.velocity.x) > 0f)
        {
            state = PlayerState.Running;
            sprite.flipX = rb.velocity.x < 0f;
        }
        else
        {
            state = PlayerState.Idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = PlayerState.Jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = PlayerState.Falling;
        }

        animator.SetInteger("State", (int)state);
    }

    // الكشف عن وجود اللاعب على الأرض
    private bool IsOnGround()
    {
        return Physics2D.BoxCast(collid.bounds.center, collid.bounds.size, 0f, Vector2.down, .1f, jumpingLayer);
    }

    // اكتشاف اصطدام اللاعب بالآلة وتفعيل الطائرة
    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("Machine"))
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                rb.bodyType = RigidbodyType2D.Static;
                plane.SetActive(true);
            }
        }
    }
}