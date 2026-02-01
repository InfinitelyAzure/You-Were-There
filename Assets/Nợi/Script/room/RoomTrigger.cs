using cherrydev;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    [SerializeField] private DialogBehaviour dialogPrefab;
    [SerializeField] private DialogNodeGraph scriptable;
    public RoomID roomID;

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
