using cherrydev;
using UnityEngine;
using UnityEngine.Events;
public class Interactable : MonoBehaviour
{
    [Tooltip("Bỏ DialogPregab trong Hierarchy vô đây")][SerializeField] public DialogBehaviour dialogPrefab;
    [Tooltip("Bỏ Scriptable lời thoại vô đây")][SerializeField] private DialogNodeGraph scriptable;
    public InteractType interactType;
    public Transform PointNUM;
    public float CharFaceY=-1;
    public float CharFaceX=-1;
    public int Sorting=0;
    [Header("UI")]
    [TextArea]
    public string interactText = "Nhấn [E] để tương tác";

    [Header("Settings")]
    public bool canInteract = true;
    
    public string GetText()
    {
        return interactText;
    }

    public void Interact()
    {
        dialogPrefab.StartDialog(scriptable);
    }
}
