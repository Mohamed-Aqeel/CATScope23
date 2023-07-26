using UnityEngine;

public class Jumper: MonoBehaviour
{
    public float jumpForce = 10f; // القوة التي سيتم دفع اللاعب بها
    public AudioClip jumpSound; // صوت عندما يلامس اللاعب الكائن
    public bool jumpInOppositeDirection = false; // هل يجب دفع اللاعب في الاتجاه المعاكس لاتجاه دخوله إلى المنطقة

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // التأكد من أن اللاعب هو الذي لمس الكائن
        {
            // تشغيل صوت القفز
            AudioSource.PlayClipAtPoint(jumpSound, transform.position);

            Rigidbody2D playerRigidbody = collision.gameObject.GetComponent<Rigidbody2D>(); // الحصول على مكون Rigidbody2D في اللاعب

            // دفع اللاعب بالاتجاه المناسب
            if (jumpInOppositeDirection)
            {
                Vector2 jumpDirection = playerRigidbody.position - (Vector2)transform.position;
                playerRigidbody.AddForce(jumpDirection.normalized * jumpForce, ForceMode2D.Impulse);
            }
            else
            {
                playerRigidbody.velocity = Vector2.up * jumpForce;
            }
        }
    }
}
