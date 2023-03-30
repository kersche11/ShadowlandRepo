using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetingSate : PlayerBaseState
{
    private Vector2 dodgingDirectionInput;
    private float remainingDodgeTime;

    //Speichert die Target Animation als Hash in den integer
    private readonly int TargetingBlendTreeHash = Animator.StringToHash("TargetingBlendTree");
    private readonly int TargetingForwardTreeHash = Animator.StringToHash("TargetingForward");
    private readonly int TargetingRightTreeHash = Animator.StringToHash("TargetingRight");
    public PlayerTargetingSate(PlayerStateMachine stateMachine) : base(stateMachine){}

    private const float CrossFadeDuration =0.1f;
    public override void Enter()
    {
        //Subscribe das Cancel Event
        stateMachine.InputReader.CancelEvent += OnCancel;
        stateMachine.InputReader.JumpEvent += OnJump;
        stateMachine.InputReader.DodgeEvent += OnDodge;

        //Starte Target Animation
        stateMachine.Animator.CrossFadeInFixedTime(TargetingBlendTreeHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        //Wenn der Player den Attack Button (Left-Click, Button West) dr�ckt
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
        stateMachine.InputReader.CancelEvent -= OnCancel;
        stateMachine.InputReader.JumpEvent -= OnJump;
        stateMachine.InputReader.DodgeEvent -= OnDodge;
    }

    //Nach dem dr�cken der Escapetaste wechseln wir wieder vom TargetModus in den PlayerFreeLookState
    //Zerst wir das CurrentTarget auf "Null" gesetzt, dann erst gewechselt
    private void OnCancel()
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

        if (remainingDodgeTime >0f)
        {
            movement += stateMachine.transform.right * dodgingDirectionInput.x * stateMachine.DodgeDistance / stateMachine.DodgeDuration;
            movement += stateMachine.transform.forward * dodgingDirectionInput.y* stateMachine.DodgeDistance / stateMachine.DodgeDuration;
            
            remainingDodgeTime -= deltaTime;

            if (remainingDodgeTime < 0f) { remainingDodgeTime = 0f; }

        }
        else
        {
            movement += stateMachine.transform.right * stateMachine.InputReader.MovementValue.x;
            movement += stateMachine.transform.forward * stateMachine.InputReader.MovementValue.y;

        }


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
    
    
    
    //Wenn der Player doding (STRG) verwendet hat diese F�higkeit 1 sekunde cooldown zeit
    private void OnDodge()
    {
        if (Time.time - stateMachine.PreviousDodgeTime < stateMachine.DodgeCoolDown){return;}

        stateMachine.SetDodgeTime(Time.time);
        dodgingDirectionInput = stateMachine.InputReader.MovementValue;
        remainingDodgeTime = stateMachine.DodgeDuration;
    }
}
