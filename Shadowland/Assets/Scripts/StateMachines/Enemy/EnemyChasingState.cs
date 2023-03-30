using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasingState : EnemyBaseState
{
    private readonly int LocomotionBlendTreeHash = Animator.StringToHash("Locomotion");

    private readonly int speedHash = Animator.StringToHash("Speed");

    private const float CrossFadeDuration = 0.1f;
    private const float AnimatorDampTime = 0.1f;
    public EnemyChasingState(EnemyStateMachine stateMachine) : base(stateMachine){}

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(LocomotionBlendTreeHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        if (!IsInChaseRange())
        {
            stateMachine.SwitchState(new EnemyIdleState(stateMachine));
            return;
        }
        else if (IsInAttackRange())
        {
            Debug.Log("Attack");
            stateMachine.SwitchState(new EnemyAttackingState(stateMachine));
        }
        MoveToPlayer(deltaTime);
        FacePlayer();

        //Setze Speed des Enemies smooth auf 0 (Idle)
        stateMachine.Animator.SetFloat(speedHash, 1f, AnimatorDampTime, deltaTime);
    }

  

    public override void Exit()
    {
        stateMachine.navMeshAgent.ResetPath();
        stateMachine.navMeshAgent.velocity = Vector3.zero;
    }
    private void MoveToPlayer(float deltaTime)
    {
        //Checkt ob das NavMesh mit der Playervelosity übereinstimmt
        if (stateMachine.navMeshAgent.isOnNavMesh)
        {
            stateMachine.navMeshAgent.destination = stateMachine.Player.transform.position;

            Move(stateMachine.navMeshAgent.desiredVelocity.normalized * stateMachine.MovementSpeed, deltaTime);
        }

        //Synchronisiert NavMeshAgent mit CharacterController
        stateMachine.navMeshAgent.velocity = stateMachine.EnemyController.velocity;
    }

    private bool IsInAttackRange()
    {
        //Check ob der Player noch lebt 
        if (stateMachine.Player.IsDead){ return false; }

        float playerDistanceSqr=(stateMachine.Player.transform.position-stateMachine.transform.position).sqrMagnitude;

        return playerDistanceSqr <= stateMachine.AttackRange*stateMachine.AttackRange;
    }
}
