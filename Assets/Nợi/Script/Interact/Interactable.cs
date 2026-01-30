using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [Header("UI")]
    [TextArea]
    public string interactText = "Nhấn [E] để tương tác";

    [Header("Settings")]
    public bool canInteract = true;

    [Header("Events")]
    public UnityEvent onInteract;

    public string GetText()
    {
        return interactText;
    }

    public void Interact()
    {
        if (!canInteract) return;

        onInteract?.Invoke();
    }
}
