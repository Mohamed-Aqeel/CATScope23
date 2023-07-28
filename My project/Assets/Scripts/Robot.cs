using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Robot: MonoBehaviour
{
    [SerializeField] private float movementSpeed = 7f;
    private Rigidbody2D rb;
    private Animator anime;
    private SpriteRenderer sprite;
    private enum PlayerAnimator { Player_Stop, Player_Running, shortAttack,LongAttack }
    private BoxCollider2D collid;
    [SerializeField] private AudioSource attackSound;
    private PlayerAnimator state;
    [SerializeField] private string groundLayerName = "Ground";
    private LayerMask groundLayerMask;
    public KeyCode ShortAttacKey = KeyCode.Z;
    public KeyCode LongAttacKey = KeyCode.x;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        collid = GetComponent<BoxCollider2D>();
        groundLayerMask = LayerMask.GetMask(groundLayerName);

        // تعيين صوت الهجوم إلى الخاصية clip الخاصة بالـ AudioSource
        attackSound.clip = Resources.Load<AudioClip>("AttackSound");
    }

    private void Update()
    {
        float movement = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(movement * movementSpeed, rb.velocity.y);

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

        anime.SetInteger("State", (int)state);

        if (Input.GetButtonDown(ShortAttacKey))
        {
            state = PlayerAnimator.shortAttack;

        }
        else if (Input.GetButtonDown(LOngAttacKey))
        {
            state = PlayerAnimator.LongAttack;

        }
    }

    public void ShortRangeAttack()
    {
        // الكود الخاص بالهجوم قصير المدي

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayerMask);

        foreach (Collider2D enemy in hitEnemies)
        {
            // تفعيل الضرر على العدو هنا
        }

      attackSound.Play();
    }
    [SerializeField] private GameObject Shoot;
    [SerializeField] private Transform Pos
    public void LongRangeAttack()
    {
        // الكود الخاص بالهجوم الطويل المدي
        Instantiate(Shoot, Pos.position, Pos.rotation);
        attackSound.Play();
    }
}