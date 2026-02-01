using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    public RoomID roomID;

    private void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Player"))
    {
        RoomVisitedManager.Instance.Visit(roomID);
        QuestManager.Instance.OnPlayerEnterRoom(roomID);
    }
}
}
