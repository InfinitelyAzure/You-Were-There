using UnityEngine;
using UnityEngine.Events;
public class Interactable : MonoBehaviour
{
    public InteractType interactType;
    public Transform PointNUM;
    public float CharFace=-1;
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
        
    }
}
