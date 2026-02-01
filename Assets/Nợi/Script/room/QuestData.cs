using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Quest Data")]
public class QuestData : ScriptableObject
{
    public string questID;

    public string questTitle;

    [TextArea]
    public string questDescription;

    public RoomID targetRoom;
}
