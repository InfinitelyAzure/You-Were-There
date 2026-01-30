using UnityEngine;
using cherrydev;

public class TestDialogStarter : MonoBehaviour
{
    [SerializeField] private DialogBehaviour dogshit;
    [SerializeField] private DialogNodeGraph SLEEEPY;
    private void Start()
    {
        dogshit.BindExternalFunction("Test", METHOD);
        dogshit.StartDialog(SLEEEPY);
    }
    private void METHOD()
    {
        Debug.Log("test test test");
    }
    
}