using UnityEngine;

public class Jumper: MonoBehaviour
{
    public float jumpForce = 10f; // القوة التي سيتم دفع اللاعب بها

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Jumper")) // التأكد من أن اللاعب هو الذي لمس الكائن
        {
                // الحصول على المكون Rigidbody2D للاعب
                Rigidbody2D rb = GetComponent<Rigidbody2D>();

                // تطبيق القوة للقفز
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            
        }
    }

}
