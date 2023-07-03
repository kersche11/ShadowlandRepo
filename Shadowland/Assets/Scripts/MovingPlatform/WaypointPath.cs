using UnityEngine;

public class WaypointPath : MonoBehaviour
{
    int nextWaypointIndex;
    public Transform GetWaypoint(int waypointIndex)
    {
        Debug.Log(waypointIndex);
        return transform.GetChild(waypointIndex);
    }

    public int GetNextWaypointIndex(int currentWaypointIndex)
    {

        if (nextWaypointIndex < transform.childCount)
        {
            nextWaypointIndex = currentWaypointIndex + 1;
        }

        return nextWaypointIndex;

    }
}
