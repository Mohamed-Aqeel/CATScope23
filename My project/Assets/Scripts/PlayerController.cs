using UnityEngine;

// تحكم في حركة اللاعب وهجومه وقفزه
public class PlayerController: MonoBehaviour
{
    // سرعة الحركة
    public float moveSpeed = 5f;

    // مفاتيح الهجوم والقفز
    public KeyCode attackKey = KeyCode.Space;
    public KeyCode jumpKey = KeyCode.E;

    // مؤشر الرسوم المتحركة
    private Animator animator;

    // التحقق من وجود مؤشر الرسوم المتحركة
    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("اللاعب لا يحتوي على مؤشر Animator.");
        }
    }

    // التحكم في حركة اللاعب
    public void Move(float x, float y)
    {
        Vector2 movement = new Vector2(x, y).normalized * moveSpeed;
        transform.Translate(movement * Time.deltaTime);

        // تحديث رسوم اللاعب
        if (animator != null)
        {
            animator.SetFloat("Horizontal", x);
            animator.SetFloat("Vertical", y);
            animator.SetFloat("Speed", movement.magnitude);
        }
    }

    // التحكم في هجوم اللاعب
    public void Attack()
    {
        // تشغيل الرسوم المتحركة للهجوم
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }

        // إطلاق صوت الهجوم
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    // التحكم في قفز اللاعب
    public void Jump()
    {
        // تشغيل الرسوم المتحركة للقفز
        if (animator != null)
        {
            animator.SetTrigger("Jump");
        }
    }
}