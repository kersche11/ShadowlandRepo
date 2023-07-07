using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipController : MonoBehaviour
{
    [SerializeField]
    private WaypointPath _waypointPath;

    [SerializeField]
    private float _speed;

   
    private int _targetWaypointIndex;

    private Transform _previousWaypoint;
    private Transform _targetWaypoint;

    private float _timeToWaypoint;
    private float _elapsedTime;

    public bool gameIsOn = false;
    private bool start = false;



    void FixedUpdate()
    {
        if (gameIsOn)
        {
            if (!start)
            {
                start = true;
                TargetNextWaypoint();
            }
           

            _elapsedTime += Time.deltaTime;

            float elapsedPercentage = _elapsedTime / _timeToWaypoint;
            elapsedPercentage = Mathf.SmoothStep(0, 1, elapsedPercentage);
            transform.position = Vector3.Lerp(_previousWaypoint.position, _targetWaypoint.position, elapsedPercentage);
            transform.rotation = Quaternion.Lerp(_previousWaypoint.rotation, _targetWaypoint.rotation, elapsedPercentage);

            if (elapsedPercentage >= 1)
            {
                //TargetNextWaypoint();
                SceneManager.LoadScene("OpenWorld");
                LevelManager.Instance.ResetDiamondCount();
            }
            
        }
    }

    //Abfragen der Transforms von Start und Zieltarget
    private void TargetNextWaypoint()
    {
        _previousWaypoint = _waypointPath.GetWaypoint(_targetWaypointIndex);
        _targetWaypointIndex = _waypointPath.GetNextWaypointIndex(_targetWaypointIndex);
        _targetWaypoint = _waypointPath.GetWaypoint(_targetWaypointIndex);

        _elapsedTime = 0;

        float distanceToWaypoint = Vector3.Distance(_previousWaypoint.position, _targetWaypoint.position);
        _timeToWaypoint = distanceToWaypoint / _speed;

    }


    void MoveHierarchy(GameObject obj, GameObject newParent)
    {
        obj.transform.SetParent(newParent.transform, true);
        foreach (Transform child in obj.transform)
        {
            MoveHierarchy(child.gameObject, newParent);
        }
    }
}
