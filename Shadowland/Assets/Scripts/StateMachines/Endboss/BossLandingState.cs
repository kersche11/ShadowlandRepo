using UnityEngine;

public class BossLandingState : BossBaseState
{

    private int _targetWaypointIndex;

    private Transform _previousWaypoint;
    private Transform _targetWaypoint;

    private float _timeToWaypoint;
    private float _elapsedTime;

    private Vector3 _currentWaypointPosition;
    private Quaternion _currentWaypointRotation;

    private readonly int flyHash = Animator.StringToHash("flying");
    private const float CrossFadeDuration = 0.1f;
    public BossLandingState(BossStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("Enter Landing State");
        stateMachine.Animator.CrossFadeInFixedTime(flyHash, CrossFadeDuration);
        TargetNextWaypoint();

    }



    public override void Tick(float deltaTime)
    {
       

        if (Vector3.Distance(stateMachine.WaypointCenter.transform.position, _previousWaypoint.position) < 0.01f)
        {
            TargetNextWaypoint();
            // _currentWaypointIndex = (_currentWaypointIndex + 1) % waypoints.Length;
        }
        else
        {
            stateMachine.WaypointCenter.transform.position = Vector3.MoveTowards(
                stateMachine.WaypointCenter.transform.position,
                _previousWaypoint.position,
                stateMachine.MovementSpeed * Time.deltaTime);
            stateMachine.WaypointCenter.transform.LookAt(_previousWaypoint.position);
        }

        _elapsedTime += Time.deltaTime;

        float elapsedPercentage = _elapsedTime / _timeToWaypoint;
        elapsedPercentage = Mathf.SmoothStep(0, 1, elapsedPercentage);
        //stateMachine.WaypointCenter.transform.position = Vector3.MoveTowards(_previousWaypoint.position, _targetWaypoint.position, elapsedPercentage);
        stateMachine.WaypointCenter.transform.rotation = Quaternion.Lerp(_previousWaypoint.rotation, _targetWaypoint.rotation, elapsedPercentage);

        //abfrage ob er schon den letzten waypoint erreicht hat
        if (_targetWaypointIndex == stateMachine.GetTotalWaypoints() - 1 && Vector3.Distance(stateMachine.WaypointCenter.transform.position, _targetWaypoint.position) < 0.01f)
        {
            stateMachine.SwitchState(new BossIdleState(stateMachine));
        }


    }
    public override void Exit()
    {
       
        //stateMachine.WaypointPathLanding.enabled = false;
    }

    private void TargetNextWaypoint()
    {
        _previousWaypoint = stateMachine.WaypointPathLanding.GetWaypoint(_targetWaypointIndex);
        _targetWaypointIndex = stateMachine.WaypointPathLanding.GetNextWaypointIndex(_targetWaypointIndex);
        _targetWaypoint = stateMachine.WaypointPathLanding.GetWaypoint(_targetWaypointIndex);

        _elapsedTime = 0;

        float distanceToWaypoint = Vector3.Distance(_previousWaypoint.position, _targetWaypoint.position);
        _timeToWaypoint = distanceToWaypoint / stateMachine.MovementSpeed;

    }

    

   
}
