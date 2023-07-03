using UnityEngine;

public class BossPatrollState : BossBaseState
{


    private int _targetWaypointIndex;

    private int _targetCount = 0;
    private Transform _previousWaypoint;
    private Transform _targetWaypoint;

    private float _timeToWaypoint;
    private float _elapsedTime;

    private Vector3 _currentWaypointPosition;
    private Quaternion _currentWaypointRotation;

    private readonly int flyHash = Animator.StringToHash("flying");
    private readonly int glideHash = Animator.StringToHash("glide");
    private const float CrossFadeDuration = 0.1f;

    public BossPatrollState(BossStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        _targetCount = 0;
        stateMachine.Animator.CrossFadeInFixedTime(flyHash, CrossFadeDuration);
        stateMachine.startFightTrigger.FightEvent += OnFight;
        TargetNextWaypoint();
    }



    public override void Tick(float deltaTime)
    {

        if (_targetCount == 3)
        {
            stateMachine.Animator.CrossFadeInFixedTime(glideHash, CrossFadeDuration);
        }

        if (Vector3.Distance(stateMachine.WaypointCenter.transform.position, _previousWaypoint.position) < 0.01f)
        {

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


        //if (elapsedPercentage >= 1)
        //{
        //    if (_targetWaypointIndex<16)
        //    {
        //        TargetNextWaypoint();
        //    }

        //}

        if (_targetCount == 4)
        {
            stateMachine.Animator.CrossFadeInFixedTime(flyHash, CrossFadeDuration);
        }

    }

    public override void Exit()
    {
        stateMachine.startFightTrigger.FightEvent -= OnFight;
    }

    //Abfragen der Transforms von Start und Zieltarget
    private void TargetNextWaypoint()
    {
        _targetCount++;
        _previousWaypoint = stateMachine.WaypointPathPatroll.GetWaypoint(_targetWaypointIndex);
        _targetWaypointIndex = stateMachine.WaypointPathPatroll.GetNextWaypointIndex(_targetWaypointIndex);
        _targetWaypoint = stateMachine.WaypointPathPatroll.GetWaypoint(_targetWaypointIndex);

        _elapsedTime = 0;

        float distanceToWaypoint = Vector3.Distance(_previousWaypoint.position, _targetWaypoint.position);
        _timeToWaypoint = distanceToWaypoint / stateMachine.MovementSpeed;

    }

    private void MoveToWaypoint(float deltaTime)
    {



        ////Checkt ob das NavMesh mit der Playervelosity übereinstimmt
        //if (stateMachine.navMeshAgent.isOnNavMesh)
        //{
        //    stateMachine.navMeshAgent.destination = _currentWaypointPosition;
        //    stateMachine.EnemyController.Move(_currentWaypointPosition * deltaTime);

        //}

        //Synchronisiert NavMeshAgent mit CharacterController
        //stateMachine.navMeshAgent.velocity = stateMachine.EnemyController.velocity;
    }

    private void OnFight()
    {
        stateMachine.SwitchState(new BossLandingState(stateMachine));
    }
}
