using UnityEngine;

public class DeathKnight : MonoBehaviour
{
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private float movementSpeed = 7f;
    private Rigidbody2D rb;
    private Animator anime;
    private float movement;
    private SpriteRenderer sprite;
    private enum PlayerAnimator { Player_Stop, Player_Running, Player_Jumping, Player_Falling, Player_Attack }
    private BoxCollider2D collid;
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource fallSound;
    [SerializeField] private AudioSource swordSound;
    private PlayerAnimator state;
    [SerializeField] private string groundLayerName = "Ground";
    private LayerMask groundLayerMask;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private LayerMask enemyLayerMask;
    private bool canAttack = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        collid = GetComponent<BoxCollider2D>();
        groundLayerMask = LayerMask.GetMask(groundLayerName);

        jumpSound.clip = Resources.Load<AudioClip>("WalkSound");
        fallSound.clip = Resources.Load<AudioClip>("FallSound");
        swordSound.clip = Resources.Load<AudioClip>("SwordSound");
    }

    private void Update()
    {
        movement = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(movement * movementSpeed, rb.velocity.y);

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

        if (Input.GetKeyDown("z"))
        {
            State = PlayerAnimator.Player_Attack;
        }
        anime.SetInteger("State", (int)State);

    }

    private void Attack()
    {

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayerMask);

        foreach (Collider2D enemy in hitEnemies)
        {
            // تفعيل الضرر على العدو هنا
        }

        swordSound.Play();

    }

   
}