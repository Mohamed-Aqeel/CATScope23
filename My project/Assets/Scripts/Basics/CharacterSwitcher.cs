using UnityEngine;

public class CharacterSwitcher : MonoBehaviour
{
    // الشخصيات
    public GameObject[] characters;

    // مفتاح التحويل
    public KeyCode switchKey = KeyCode.Tab;

    // مفاتيح التحويل بين الشخصيات
    public KeyCode[][] characterSwitchKeys;

    // مؤشر الشخصية الحالية
    private int currentCharacterIndex = 0;

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

      /*  PlayerController pc = characters[currentCharacterIndex].GetComponent<PlayerController>();
        if (pc == null)
        {
            Debug.LogError("الشخصية الحالية لا تحتوي على مكون PlayerController.");
        }*/
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

            // إظهار الشخصية الجديدة و التأكد من أنها لديها PlayerController
            characters[currentCharacterIndex].SetActive(true);
            characters[currentCharacterIndex].transform.position = characters[(currentCharacterIndex + characters.Length - 1) % characters.Length].transform.position;
            /*PlayerController pc = characters[currentCharacterIndex].GetComponent<PlayerController>();
            if (pc == null)
            {
                Debug.LogError("الشخصية الحالية لا تحتوي على مكون PlayerController.");
            }*/
        }

        

        // التحويل بين الشخصيات بواسطة مفاتيح مخصصة
        for (int i = 0; i < characterSwitchKeys[currentCharacterIndex].Length; i++)
        {
            if (Input.GetKeyDown(characterSwitchKeys[currentCharacterIndex][i]))
            {
                // إخفاء جميع الشخصيات باستثناء الشخصية المحددة
                for (int j = 0; j < characters.Length; j++)
                {
                    if (j == i)
                    {
                        characters[j].SetActive(true);
                    }
                    else
                    {
                        characters[j].SetActive(false);
                    }
                }

                // تحديد الشخصية المحددة كشخصية حالية
                currentCharacterIndex = i;

                // التأكد من أن الشخصية الحالية لديها PlayerController
               /* PlayerController pc = characters[currentCharacterIndex].GetComponent<PlayerController>();
                if (pc == null)
                {
                    Debug.LogError("الشخصية الحالية لا تحتوي على مكون PlayerController.");
                }*/

                break;
            }
        }
    }
}