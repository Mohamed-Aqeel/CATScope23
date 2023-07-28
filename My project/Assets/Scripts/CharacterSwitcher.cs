/*using UnityEngine;
public class CharacterSwitcher : MonoBehaviour
{
    // الشخصيات
    public GameObject[] characters;

    // مفتاح التحويل
    public KeyCode switchKey = KeyCode.Tab;

    // مفاتيح التحويل بين الشخصيات
    public KeyCode[][] characterSwitchKeys;

    // نقاط الصحة والدرع لجميع الشخصيات
    public int maxHealth = 200;
    public int maxArmor = 200;

    // مؤشر الشخصية الحالية
    private int currentCharacterIndex = 0;

    // نقاط الصحة والدرع للشخصية الحالية
    private int currentHealth;
    private int currentArmor;

    // التحقق من أن الشخصية الحالية لديها PlayerController
    void Start()
    {
        // إظهار الشخصية الأولى وإخفاء الأخرى
        for (int i = 0; i < characters.Length; i++)
        {
            if (i == currentCharacterIndex)
            {
                characters[i].SetActive(true);
            }
            else
            {
                characters[i].SetActive(false);
            }
        }

        PlayerController pc = characters[currentCharacterIndex].GetComponent<PlayerController>();
        if (pc == null)
        {
            Debug.LogError("الشخصية الحالية لا تحتوي على مكون PlayerController.");
        }

        currentHealth = maxHealth;
        currentArmor = (currentCharacterIndex == 0) ? maxArmor : 0;
    }

    void Update()
    {
        // التحويل بين الشخصيات
        if (Input.GetKeyDown(switchKey))
        {
            // إخفاء الشخصية الحالية
            characters[currentCharacterIndex].SetActive(false);

            // الانتقال إلى الشخصية التالية
            currentCharacterIndex = (currentCharacterIndex + 1) % characters.Length;

            // إظهار الشخصية الجديدة وتحديث نقاط الصحة والدرع
            characters[currentCharacterIndex].SetActive(true);
            PlayerController pc = characters[currentCharacterIndex].GetComponent<PlayerController>();
            if (pc == null)
            {
                Debug.LogError("الشخصية الحالية لا تحتوي على مكون PlayerController.");
            }

            currentHealth = maxHealth;
            currentArmor = (currentCharacterIndex == 0) ? maxArmor : 0;
        }

        // التحقق من لمس اللاعب أو العدو
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);
        foreach (Collider2D collider in colliders)
        {
            // لمس اللاعب
            if (collider.CompareTag("Player"))
            {
                currentHealth -= 200;
                if (currentHealth <= 0)
                {
                    // إظهار انميشن الموت
                    Debug.Log("اللاعب مات!");
                }
            }

            // لمس العدو
            if (collider.CompareTag("Enemy"))
            {
                int damage = 20;
                if (currentCharacterIndex == 0 && currentArmor > 0)
                {
                    currentArmor -= damage;
                    if (currentArmor < 0)
                    {
                        currentArmor = 0;
                        currentHealth -= damage;
                    }
                }
                else
                {
                    currentHealth -= damage;
                }

                if (currentHealth <= 0)
                {
                    // إظهار انميشن الموت
                    Debug.Log("اللاعب مات!");
                }
            }
        }
    }
    public int GetCurrentCharacterIndex()
    {
        return currentCharacterIndex;
    }
}*/