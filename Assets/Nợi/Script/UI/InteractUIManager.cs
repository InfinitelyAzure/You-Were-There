using System.Collections;
using cherrydev;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractUIManager : MonoBehaviour
{
    public static InteractUIManager Instance;
    [SerializeField] public DialogBehaviour dialogPrefab;
    public GameObject panel;
    public GameObject TelePoints;
    public TMP_Text interactText;
    public Image PanelDark;
    public float duration = 1f;

    void Awake()
    {
        Instance = this;
        Hide();
        dialogPrefab.BindExternalFunction("FadeOut", FadeOut);
        
    }

    public void Show(string text)
    {
        Debug.Log("AA13");
        panel.SetActive(true);
        interactText.text = text;
    }

    public void Hide()
    {
        panel.SetActive(false);
    }
    public void FadeOut()
    {
        StopAllCoroutines();
        StartCoroutine(Fade(1f, 0f));
    }

    IEnumerator Fade(float from, float to)
    {
        float time = 0f;
        Color color = PanelDark.color;

        while (time < duration)
        {
            time += Time.deltaTime;
            color.a = Mathf.Lerp(from, to, time / duration);
            PanelDark.color = color;
            yield return null;
        }

        color.a = to;
        PanelDark.color = color;
        if (PanelDark.color.a == 0f) TelePoints.SetActive(true);
        
    }
}
