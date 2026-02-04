using TMPro;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;

    private void Start()
    {
        QuestManager.Instance.OnQuestUpdated += UpdateUI;
        UpdateUI();
    }

    private void UpdateUI()
    {
        QuestData quest = QuestManager.Instance.CurrentQuest;

        if (quest == null)
        {
            panel.SetActive(false);
            return;
        }

        panel.SetActive(true);
        titleText.text = quest.questTitle;
        descriptionText.text = quest.questDescription;
    }
}
