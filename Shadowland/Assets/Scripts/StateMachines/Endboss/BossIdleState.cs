using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleState : BossBaseState
{
    private readonly int LocomotionBlendTreeHash = Animator.StringToHash("Locomotion");

    private readonly int speedHash = Animator.StringToHash("Speed");

    private const float CrossFadeDuration = 0.1f;
    private const float AnimatorDampTime = 0.1f;
    public BossIdleState(BossStateMachine stateMachine) : base(stateMachine){}

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(LocomotionBlendTreeHash,CrossFadeDuration);
       
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        if(IsInChaseRange())
        {
            stateMachine.SwitchState(new BossChasingWalkState(stateMachine));
            return;
        }

        FacePlayer();

        //Setze Speed des Enemies smooth auf 0 (Idle)
        stateMachine.Animator.SetFloat(speedHash, 0f, AnimatorDampTime,deltaTime);
    }

    public override void Exit()
    {
        
    }

    
}
