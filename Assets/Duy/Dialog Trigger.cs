using cherrydev;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    [SerializeField] private DialogBehaviour dialogBehavior;
    [SerializeField] private DialogNodeGraph dialogGraph;
    private void Start()
    {
        dialogBehavior.StartDialog(dialogGraph);
    }
}
