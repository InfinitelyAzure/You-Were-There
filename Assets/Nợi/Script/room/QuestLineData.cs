using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Quest/Quest Line")]
public class QuestLineData : ScriptableObject
{
    public string questLineID;
    public List<QuestData> quests;
}
