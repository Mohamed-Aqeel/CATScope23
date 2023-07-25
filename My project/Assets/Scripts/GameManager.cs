using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] players; // مصفوفة تحتوي على جميع اللاعبين الممكن اختيارهم
    private GameObject currentPlayer; // اللاعب الحالي
    public Button selectCharacterButton; // زر عائم لتحديد اللاعب المراد عرضه

    private void Start()
    {
        selectCharacterButton.onClick.AddListener(OnSelectCharacterButtonClicked);
    }

    private void OnSelectCharacterButtonClicked()
    {
        // اختيار اللاعب المراد عرضه (على سبيل المثال، اللاعب الأول)
        int selectedPlayerIndex = 0;

        // إخفاء اللاعب الحالي
        if (currentPlayer != null && currentPlayer.activeSelf)
        {
            currentPlayer.SetActive(false);
        }

        // عرض اللاعب المختار
        if (!players[selectedPlayerIndex].activeSelf)
        {
            players[selectedPlayerIndex].SetActive(true);
            players[selectedPlayerIndex].GetComponent<PlayerMovement>().enabled = true;
            players[selectedPlayerIndex].GetComponent<cameraController>().gameObject.SetActive(false);
            currentPlayer = players[selectedPlayerIndex];
        }
    }
}