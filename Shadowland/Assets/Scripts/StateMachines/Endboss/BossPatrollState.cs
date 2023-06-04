using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPatrollState : BossBaseState
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

    public BossPatrollState(BossStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(flyHash, CrossFadeDuration);
        TargetNextWaypoint();
    }

    public override void Exit()
    {
        throw new System.NotImplementedException();
    }

    public override void Tick(float deltaTime)
    {
        _elapsedTime += Time.deltaTime;

        float elapsedPercentage = _elapsedTime / _timeToWaypoint;
        elapsedPercentage = Mathf.SmoothStep(0, 1, elapsedPercentage);
        stateMachine.WaypointCenter.transform.position = Vector3.Lerp(_previousWaypoint.position, _targetWaypoint.position, elapsedPercentage);
        stateMachine.WaypointCenter.transform.rotation = Quaternion.Lerp(_previousWaypoint.rotation, _targetWaypoint.rotation, elapsedPercentage);

       
        if (elapsedPercentage >= 1)
        {
            if (_targetWaypointIndex<16)
            {
                TargetNextWaypoint();
            }
            
        }
       
    }


    //Abfragen der Transforms von Start und Zieltarget
    private void TargetNextWaypoint()
    {
        _previousWaypoint = stateMachine.WaypointPath.GetWaypoint(_targetWaypointIndex);
        _targetWaypointIndex = stateMachine.WaypointPath.GetNextWaypointIndex(_targetWaypointIndex);
        _targetWaypoint = stateMachine.WaypointPath.GetWaypoint(_targetWaypointIndex);

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

}
