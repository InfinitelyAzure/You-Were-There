using cherrydev;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    [Tooltip("Bỏ prefab vô đây")][SerializeField] private DialogBehaviour ppap;
    [Tooltip("Bỏ Scriptable vô đây")][SerializeField] private DialogNodeGraph scriptable;
    private void Start()
    {
        ppap.StartDialog(scriptable);
    }
}