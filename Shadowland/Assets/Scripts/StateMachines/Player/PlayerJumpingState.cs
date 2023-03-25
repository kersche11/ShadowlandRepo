using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayerJumpingState : PlayerBaseState
{
    private readonly int JumpHash = Animator.StringToHash("testJump");

    private Vector3 momentum;

    private const float CrossFadeDuration = 0.1f;
    public PlayerJumpingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        momentum = stateMachine.CharacterController.velocity;
        momentum.y = 0f;

      stateMachine.Animator.CrossFadeInFixedTime(JumpHash, CrossFadeDuration);  
       
    }
    public override void Tick(float deltaTime)
    {
        
    }

    public override void Exit()
    {
       
    }

  
 
}
