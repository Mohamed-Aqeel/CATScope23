/*using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject characterManager; // مرجع إلى كائن الإدارة الذي يحتوي على جميع الشخصيات
    public Vector3 offset; // الإزاحة بين اللاعب والكاميرا
    public float smoothSpeed = 0.125f; // سرعة تحريك الكاميرا

    private Transform target; // مرجع اللاعب الحالي

    void Start()
    {
        // الحصول على مرجع اللاعب الحالي
        if (characterManager != null)
        {
            CharacterSwitcher characterSwitcher = characterManager.GetComponent<CharacterSwitcher>();
            if (characterSwitcher != null && characterSwitcher.characters.Length > 0)
            {
                target = characterSwitcher.characters[characterSwitcher.GetCurrentCharacterIndex()].transform;
            }
            else
            {
                Debug.LogError("يجب أن يحتوي مرجع الإدارة على مكون CharacterSwitcher وعلى الأقل شخصية واحدة.");
            }
        }
        else
        {
            Debug.LogError("لم يتم تعيين مرجع الإدارة.");
        }
    }

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.TransformPoint(offset); // حساب موقع الكاميرا المطلوب بالنسبة لمكان الشخصية الحالية
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); // تحريك الكاميرا بشكل سلس
            transform.position = smoothedPosition;

            transform.LookAt(target); // توجيه الكاميرا نحو الشخصية الحالية
        }
    }
}*/