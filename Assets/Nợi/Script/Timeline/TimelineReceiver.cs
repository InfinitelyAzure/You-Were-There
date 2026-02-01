using cherrydev;
using UnityEngine;

public class TimelineReceiver : MonoBehaviour
{
    [Header("Timeline Events")]
    [SerializeField] private DialogBehaviour dialogPrefab;
    [SerializeField] private DialogNodeGraph[] scriptable;

    public void Event01() => dialogPrefab.StartDialog(scriptable[0]);
    public void Event02() => dialogPrefab.StartDialog(scriptable[1]);
    public void Event03() => dialogPrefab.StartDialog(scriptable[2]);
    public void Event04() => dialogPrefab.StartDialog(scriptable[3]);
    public void Event05() => dialogPrefab.StartDialog(scriptable[4]);
    
}
