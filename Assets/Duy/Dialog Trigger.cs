using cherrydev;
using UnityEngine;

public class DialogTest : MonoBehaviour
{
    [Tooltip("Bỏ DialogPregab trong Hierarchy vô đây")][SerializeField] private DialogBehaviour dialogPrefab;
    [Tooltip("Bỏ Scriptable lời thoại vô đây")][SerializeField] private DialogNodeGraph scriptable;
    private void Start()
    {
        dialogPrefab.StartDialog(scriptable);
    }
}