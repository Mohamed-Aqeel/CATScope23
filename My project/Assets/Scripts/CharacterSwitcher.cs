using UnityEngine;

public class CharacterSwitcher: MonoBehaviour
{
    // الشخصيات
    public GameObject[] characters;

    // مفتاح التحويل
    public KeyCode switchKey = KeyCode.Tab;

    // مفاتيح التحويل بين الشخصيات
    public KeyCode[] characterSwitchKeys;

    // مؤشر الشخصية الحالية
    private int currentCharacterIndex = 0;

    // التحقق من أن الشخصية الحالية لديها PlayerController
    void Start()
    {
        PlayerController pc = characters[currentCharacterIndex].GetComponent<PlayerController>();
        if (pc == null)
        {
            Debug.LogError("الشخصية الحالية لا تحتوي على مكون PlayerController.");
        }

        // إخفاء جميع الشخصيات باستثناء الشخصية الحالية
        for (int i = 0; i < characters.Length; i++)
        {
            if (i != currentCharacterIndex)
            {
                characters[i].SetActive(false);
            }
        }
    }

    void Update()
    {
        // التحويل بين الشخصيات
        if (Input.GetKeyDown(switchKey))
        {
            // إخفاء الشخصية الحالية
            characters[currentCharacterIndex].SetActive(false);

            // إظهار الشخصية الأولى
            currentCharacterIndex = 0;

            // إظهار الشخصية الجديدة و التأكد من أنها لديها PlayerController
            characters[currentCharacterIndex].SetActive(true);
            PlayerController pc = characters[currentCharacterIndex].GetComponent<PlayerController>();
            if (pc == null)
            {
                Debug.LogError("الشخصية الحالية لا تحتوي على مكون PlayerController.");
            }

            // إخفاء جميع الشخصيات الأخرى
            for (int i = 0; i < characters.Length; i++)
            {
                if (i != currentCharacterIndex)
                {
                    characters[i].SetActive(false);
                }
            }
        }

        // التحكم في الشخصية الحالية
        PlayerController currentPC = characters[currentCharacterIndex].GetComponent<PlayerController>();
        currentPC.Move(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetKeyDown(currentPC.attackKey))
        {
            currentPC.Attack();
        }

        if (Input.GetKeyDown(currentPC.jumpKey))
        {
            currentPC.Jump();
        }

        for (int i = 0; i < characterSwitchKeys.Length; i++)
        {
            if (Input.GetKeyDown(characterSwitchKeys[i]))
            {
                // إخفاء الشخصية الحالية
                characters[currentCharacterIndex].SetActive(false);

                // إظهار الشخصية المرادة
                currentCharacterIndex = i;

                // إظهار الشخصية الجديدة و التأكد من أنها لديها PlayerController
                characters[currentCharacterIndex].SetActive(true);
                PlayerController pc = characters[currentCharacterIndex].GetComponent<PlayerController>();
                if (pc == null)
                {
                    Debug.LogError("الشخصية الحالية لا تحتوي على مكون PlayerController.");
                }

                // إخفاء جميع الشخصيات الأخرى
                for (int j = 0; j < characters.Length; j++)
                {
                    if (j != currentCharacterIndex)
                    {
                        characters[j].SetActive(false);
                    }
                }

                break;
            }
        }
    }
}