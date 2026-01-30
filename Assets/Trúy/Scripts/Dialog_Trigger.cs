using cherrydev;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    [Tooltip("Bỏ prefab vô đây")][SerializeField] private DialogBehaviour fakyou;
    [Tooltip("Bỏ Scriptable vô đây")][SerializeField] private DialogNodeGraph scriptable;
    private void Start()
    {
        fakyou.StartDialog(scriptable);
    }
}