using System.Collections.Generic;
using UnityEngine;

public class RoomVisitedManager : MonoBehaviour
{
    public static RoomVisitedManager Instance;

    private HashSet<RoomID> visitedRooms = new HashSet<RoomID>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void Visit(RoomID roomID)
    {
        if (visitedRooms.Add(roomID))
        {
            Debug.Log("Player visited: " + roomID);
        }
    }

    public bool HasVisited(RoomID roomID)
    {
        return visitedRooms.Contains(roomID);
    }
}
