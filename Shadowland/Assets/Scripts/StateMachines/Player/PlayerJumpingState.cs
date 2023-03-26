using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayerJumpingState : PlayerBaseState
{
    private readonly int JumpHash = Animator.StringToHash("Jump");
    

    private Vector3 momentum;

    private const float CrossFadeDuration = 0.1f;
    public PlayerJumpingState(PlayerStateMachine stateMachine) : base(stateMachine){}

    public override void Enter()
    {
        //Player springt die eingetellte Jumpforce (VerticalVelocity) nach oben
        stateMachine.ForceReceiver.JumpForce(stateMachine.JumpForce);

        momentum = stateMachine.CharacterController.velocity;
        momentum.y = 0f;

        stateMachine.Animator.CrossFadeInFixedTime(JumpHash, CrossFadeDuration);  
       
    }
    public override void Tick(float deltaTime)
    {
        Move(momentum, deltaTime);

        //�berpr�fe ob die Y-Geschwindigkeit 0 oder weniger ist.
        //Das bedeutet wir Fallen und wechseln in den FallingState
        if (stateMachine.CharacterController.velocity.y <=0) 
        { 
            stateMachine.SwitchState(new PlayerFallingState(stateMachine));
            return;
        }

        //Wenn wir im TargetState sind wollen wir das Ziel anvisieren.
        FaceTarget();
    }

    public override void Exit()
    {
       
    }

  
 
}
