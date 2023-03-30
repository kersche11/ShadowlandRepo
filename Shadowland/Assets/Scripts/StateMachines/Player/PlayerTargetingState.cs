using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetingSate : PlayerBaseState
{

    //Speichert die Target Animation als Hash in den integer
    private readonly int TargetingBlendTreeHash = Animator.StringToHash("TargetingBlendTree");
    private readonly int TargetingForwardTreeHash = Animator.StringToHash("TargetingForward");
    private readonly int TargetingRightTreeHash = Animator.StringToHash("TargetingRight");
    public PlayerTargetingSate(PlayerStateMachine stateMachine) : base(stateMachine){}

    private const float CrossFadeDuration =0.1f;
    public override void Enter()
    {
        //Subscribe das Cancel Event
        stateMachine.InputReader.TargetEvent += OnTarget;
        stateMachine.InputReader.JumpEvent += OnJump;
        stateMachine.InputReader.DodgeEvent += OnDodge;

        //Starte Target Animation
        stateMachine.Animator.CrossFadeInFixedTime(TargetingBlendTreeHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        //Wenn der Player den Attack Button (Left-Click, Button West) drückt
        //geht er vom Tageting in den AttackState
        //Der zweite Parameter ist die Attacking ID, der Angriff beginnt mit der ersten Attacke in der AttackList
        //Array[0]
        if (stateMachine.InputReader.IsAttacking)
        {
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine,0));
            return;
        }
        if (stateMachine.InputReader.IsBlocking)
        {
            stateMachine.SwitchState(new PlayerBlockingState(stateMachine));
            return;
        }


        //Wenn der Player das Targert verliert (out of Range, oder Death) geht man 
        //Automatisch in den FreeLookState
        if (stateMachine.Targeter.CurrentTarget==null)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
        }
        //Kalkuliert die Bewegungen im TargetState

        Vector3 movement = CalculateMovement(deltaTime);
        Move(movement*stateMachine.TargetingMovementSpeed, deltaTime);


        UpdateAnimator(deltaTime);

        //Immer zum Gegner schauen
        FaceTarget();



    }
    public override void Exit()
    {
        stateMachine.InputReader.TargetEvent -= OnTarget;
        stateMachine.InputReader.JumpEvent -= OnJump;
        stateMachine.InputReader.DodgeEvent -= OnDodge;
    }

    //Nach dem drücken der Escapetaste wechseln wir wieder vom TargetModus in den PlayerFreeLookState
    //Zerst wir das CurrentTarget auf "Null" gesetzt, dann erst gewechselt
    private void OnTarget()
    {
        stateMachine.Targeter.Cancel();
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }


    private void OnJump()
    {
        stateMachine.SwitchState(new PlayerJumpingState(stateMachine));
    }


    //Im TargetModus wollen wir im (rechts) oder gegen(links) den Uhrzeigersinn um den Gegner laufen
    private Vector3 CalculateMovement(float deltaTime)
    {
        Vector3 movement = new Vector3();

        movement += stateMachine.transform.right * stateMachine.InputReader.MovementValue.x;
        movement += stateMachine.transform.forward * stateMachine.InputReader.MovementValue.y;   

        return movement;
    }

    private void UpdateAnimator(float deltaTime)
    {
        if (stateMachine.InputReader.MovementValue.x==0)
        {
            stateMachine.Animator.SetFloat(TargetingRightTreeHash, 0f, 0.1f, deltaTime);
        }
        else
        {
           float value = stateMachine.InputReader.MovementValue.x>0? 1f : -1f;
            stateMachine.Animator.SetFloat(TargetingRightTreeHash, value, 0.1f, deltaTime);
        }

        if (stateMachine.InputReader.MovementValue.y == 0)
        {
            stateMachine.Animator.SetFloat(TargetingForwardTreeHash, 0f, 0.1f, deltaTime);
        }
        else
        {
            float value = stateMachine.InputReader.MovementValue.y > 0 ? 1f : -1f;
            stateMachine.Animator.SetFloat(TargetingForwardTreeHash, value, 0.1f, deltaTime);
        }   
    }
    
    
    
    //Wenn der Player doding (STRG) verwendet hat diese Fähigkeit 1 sekunde cooldown zeit
    private void OnDodge()
    {
        //Check ob wir uns überhaupt gerade bewegen
        if (stateMachine.InputReader.MovementValue==Vector2.zero){ return; }

        stateMachine.SwitchState(new PlayerDodgingState(stateMachine, stateMachine.InputReader.MovementValue));
    }
}
