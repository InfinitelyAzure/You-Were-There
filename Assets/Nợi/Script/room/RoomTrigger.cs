using cherrydev;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    [SerializeField] private DialogBehaviour dialogPrefab;
    [SerializeField] private DialogNodeGraph scriptable;
    public RoomID roomID;
    public string questID;
    public bool autoDisableOnComplete = true;
    void Start()
    {
        QuestManager.Instance.RegisterTrigger(this);
        gameObject.SetActive(false);
    }
     public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Player"))
    {
        if(scriptable!=null) dialogPrefab.StartDialog(scriptable);
        RoomVisitedManager.Instance.Visit(roomID);
        QuestManager.Instance.OnPlayerEnterRoom(roomID);
        if(roomID==RoomID.ThuVien) AudioManager.Play("HmmID2");
        else if(roomID==RoomID.ChoNgoi ||roomID==RoomID.Home||roomID==RoomID.Home2||roomID==RoomID.Hanglang)AudioManager.Play("HmmID");

        if(roomID==RoomID.Cuthe)
        {
            other.GetComponent<PlayerMovement>().anim.SetFloat("LastY", -1);
            other.GetComponent<PlayerMovement>().enabled=false;
        } 
    }
    
}
}
