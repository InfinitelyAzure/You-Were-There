using cherrydev;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TriggerLine : MonoBehaviour
{
    [SerializeField] private DialogBehaviour dialogPrefab;
    [SerializeField] private DialogNodeGraph scriptable;
    void Awake()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            dialogPrefab.StartDialog(scriptable);
        }
    }
    public void PlayerPlay()
    {
        
    }
}
