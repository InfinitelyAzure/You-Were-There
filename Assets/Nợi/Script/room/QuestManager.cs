using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    [Header("External")]
    public InteractUIManager interactUIManager;

    [Header("Active Quest Line")]
    public QuestLineData activeQuestLine;

    int currentQuestIndex = -1;
    bool isTransitioning;

    // ===================== CURRENT QUEST =====================
    public QuestData CurrentQuest =>
        activeQuestLine != null &&
        currentQuestIndex >= 0 &&
        currentQuestIndex < activeQuestLine.quests.Count
            ? activeQuestLine.quests[currentQuestIndex]
            : null;

    // ===================== EVENTS =====================
    public delegate void QuestUpdate();
    public event QuestUpdate OnQuestUpdated;

    // ===================== TRIGGER REGISTRY =====================
    Dictionary<string, RoomTrigger> triggerMap = new();

    // ===================== FADE =====================
    [Header("Fade Panel")]
    public Image panelImage;

    [Header("Phase Time")]
    public float fadeInTime = 0.5f;
    public float holdTime = 0.5f;
    public float fadeOutTime = 0.5f;

    Coroutine fadeRoutine;

    // ===================== UNITY =====================
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        panelImage = interactUIManager.PanelDark;
        
    }
    void Start()
    {
        StartQuestLine();
    }

    // ===================== TRIGGER REGISTER =====================
    public void RegisterTrigger(RoomTrigger trigger)
    {
        if (!triggerMap.ContainsKey(trigger.questID))
            triggerMap.Add(trigger.questID, trigger);
    }

    // ===================== QUEST FLOW =====================
    public void StartQuestLine()
    {
        if (activeQuestLine == null || activeQuestLine.quests.Count == 0)
            return;

        DisableAllTriggers();

        currentQuestIndex = 0;
        ActivateCurrentQuest();

        OnQuestUpdated?.Invoke();
    }

    public void OnPlayerEnterRoom(RoomID roomID)
    {
        if (isTransitioning || CurrentQuest == null)
            return;

        if (roomID == CurrentQuest.targetRoom)
        {
            StartCoroutine(CompleteQuestRoutine());
        }
    }

    IEnumerator CompleteQuestRoutine()
    {
        isTransitioning = true;

        yield return PlayPhaseRoutine();

        DeactivateCurrentQuestTrigger();
        currentQuestIndex++;

        if (currentQuestIndex >= activeQuestLine.quests.Count)
        {
            Debug.Log("All quests completed");
            isTransitioning = false;
            OnQuestUpdated?.Invoke();
            yield break;
        }

        ActivateCurrentQuest();
        isTransitioning = false;

        OnQuestUpdated?.Invoke();
    }

    // ===================== TRIGGER CONTROL =====================
    void ActivateCurrentQuest()
    {
        if (CurrentQuest == null) return;

        if (triggerMap.TryGetValue(CurrentQuest.questID, out var trigger))
            trigger.Activate();

        Debug.Log($"Quest started: {CurrentQuest.questID}");
    }

    void DeactivateCurrentQuestTrigger()
    {
        if (CurrentQuest == null) return;

        if (triggerMap.TryGetValue(CurrentQuest.questID, out var trigger))
            trigger.Deactivate();
    }

    void DisableAllTriggers()
    {
        foreach (var trigger in triggerMap.Values)
            trigger.Deactivate();
    }

    // ===================== FADE PHASE =====================
    IEnumerator PlayPhaseRoutine()
    {
        if (fadeRoutine != null)
            StopCoroutine(fadeRoutine);

        fadeRoutine = StartCoroutine(FadePhaseRoutine());
        yield return fadeRoutine;
    }

    IEnumerator FadePhaseRoutine()
    {
        yield return Fade(0f, 1f, fadeInTime);
        yield return new WaitForSeconds(holdTime);
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
