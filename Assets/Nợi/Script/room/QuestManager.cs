using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;
    public InteractUIManager interactUIManager;

    [Header("Active Quest Line")]
    public QuestLineData activeQuestLine;

    private int currentQuestIndex = -1;
    private bool questCompleted;

    public QuestData CurrentQuest =>
        activeQuestLine != null &&
        currentQuestIndex >= 0 &&
        currentQuestIndex < activeQuestLine.quests.Count
            ? activeQuestLine.quests[currentQuestIndex]
            : null;

    public delegate void QuestUpdate();
    public event QuestUpdate OnQuestUpdated;
    [Header("Panel")]
    public Image panelImage;

    [Header("Phase Time")]
    public float fadeInTime = 0.5f;
    public float holdTime = 0.5f;
    public float fadeOutTime = 0.5f;

    Coroutine fadeRoutine;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
            panelImage=interactUIManager.PanelDark;
            
    }

    public void StartQuestLine()
    {
        if (activeQuestLine == null || activeQuestLine.quests.Count == 0)
            return;

        currentQuestIndex = 0;
        questCompleted = false;
        OnQuestUpdated?.Invoke();
    }

    public void OnPlayerEnterRoom(RoomID roomID)
    {
        if (questCompleted || CurrentQuest == null)
            return;

        if (roomID == CurrentQuest.targetRoom)
        {
            CompleteCurrentQuest();
        }
    }

    private void CompleteCurrentQuest()
    {
        PlayPhase();
        questCompleted = true;
        Debug.Log("Quest completed: " + CurrentQuest.questID);

        GoToNextQuest();
    }

    private void GoToNextQuest()
    {
        currentQuestIndex++;

        if (currentQuestIndex >= activeQuestLine.quests.Count)
        {
            Debug.Log("All quests completed");
            OnQuestUpdated?.Invoke();
            return;
        }

        questCompleted = false;
        OnQuestUpdated?.Invoke();
    }
    public void PlayPhase()
    {
        if (fadeRoutine != null)
            StopCoroutine(fadeRoutine);

        fadeRoutine = StartCoroutine(FadePhaseRoutine());
    }

    IEnumerator FadePhaseRoutine()
    {
        // Fade In
        yield return Fade(0f, 1f, fadeInTime);

        // Hold
        yield return new WaitForSeconds(holdTime);

        // Fade Out
        yield return Fade(1f, 0f, fadeOutTime);
    }

    IEnumerator Fade(float from, float to, float duration)
    {
        float t = 0f;
        Color color = panelImage.color;
        color.a = from;
        panelImage.color = color;

        while (t < duration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(from, to, t / duration);
            color.a = alpha;
            panelImage.color = color;
            yield return null;
        }

        color.a = to;
        panelImage.color = color;
    }

    void SetAlpha(float a)
    {
        Color c = panelImage.color;
        c.a = a;
        panelImage.color = c;
    }
}
