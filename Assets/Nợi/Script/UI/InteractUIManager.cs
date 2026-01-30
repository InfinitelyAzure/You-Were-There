using TMPro;
using UnityEngine;

public class InteractUIManager : MonoBehaviour
{
    public static InteractUIManager Instance;

    public GameObject panel;
    public TMP_Text interactText;

    void Awake()
    {
        Instance = this;
        Hide();
    }

    public void Show(string text)
    {
        panel.SetActive(true);
        interactText.text = text;
    }

    public void Hide()
    {
        panel.SetActive(false);
    }
}
