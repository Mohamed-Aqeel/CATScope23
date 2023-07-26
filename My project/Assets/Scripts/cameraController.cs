using UnityEngine;

public class CameraController : MonoBehaviour
{
    public string targetTag = "Player"; // اسم tag للعبة
    public Vector3 offset; // الإزاحة بين اللاعب والكاميرا
    public float smoothSpeed = 0.125f; // سرعة تحريك الكاميرا

    private Transform target; // مرجع اللاعب الحالي

    void Start()
    {
        target = GameObject.FindWithTag(targetTag).transform; // البحث عن اللاعب باستخدام الـ tag
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset; // حساب موقع الكاميرا المطلوب
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); // تحريك الكاميرا بشكل سلس
            transform.position = smoothedPosition;
        }
    }
}
