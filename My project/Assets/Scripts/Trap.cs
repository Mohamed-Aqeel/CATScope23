using UnityEngine;

public class Trap : MonoBehaviour
{
    public AudioClip trapSound; // صوت عندما يلامس اللاعب الفخ
    public Color trapColor; // لون الفخ الجديد

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // التأكد من أن اللاعب هو الذي لمس الفخ
        {
            // تشغيل صوت الفخ
            AudioSource.PlayClipAtPoint(trapSound, transform.position);

            // تغيير لون الفخ
            SpriteRenderer trapRenderer = GetComponent<SpriteRenderer>();
            trapRenderer.color = trapColor;

            // يمكنك هنا وضع الكود الخاص بموت اللاعب
            // على سبيل المثال، إعادة تحميل المشهد أو تنفيذ دالة خاصة بالموت
        }
    }
}
