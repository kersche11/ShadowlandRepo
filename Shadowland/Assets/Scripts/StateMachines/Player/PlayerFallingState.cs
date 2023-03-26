using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallingState :PlayerBaseState
{
    private readonly int FallHash = Animator.StringToHash("Fall");

    private Vector3 momentum;

    private const float CrossFadeDuration = 0.1f;
    public PlayerFallingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        momentum = stateMachine.CharacterController.velocity;
        momentum.y = 0;
        stateMachine.Animator.CrossFadeInFixedTime(FallHash, CrossFadeDuration);
        
    }

    public override void Tick(float deltaTime)
    {
        Move(momentum,deltaTime);

        //Wenn der Player am Boden landet kehrt er in den FreelookState oder TargetState zur�ck.
         if(stateMachine.CharacterController.isGrounded)
        {
            ReturnToLocomation();
        }


        FaceTarget();
    }

    public override void Exit()
    {

    }

    
}
