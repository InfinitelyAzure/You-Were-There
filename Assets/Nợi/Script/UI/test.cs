using cherrydev;
using UnityEngine;

public class test : MonoBehaviour
{
    [Tooltip("Bỏ DialogPregab trong Hierarchy vô đây")][SerializeField] private DialogBehaviour dialogPrefab;
    [Tooltip("Bỏ Scriptable lời thoại vô đây")][SerializeField] private DialogNodeGraph scriptable;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ttest()
    {
        dialogPrefab.StartDialog(scriptable);
    }
}
