using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    //BlendTree FreeLookSpeed(Animator)
    // private const string FreeLookSpeedString = "FreeLookSpeed";
    //Better Version (Holt sich den Wert als Integer vom Animator):
    private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");
    private readonly int FreeLookTreeBlendHash = Animator.StringToHash("FreeLookBlendTree");
    
    

    private const float AnimatorDampTime = 0.1f;
    private const float CrossFadeDuration = 0.1f;

    private bool shouldFade;

    public PlayerFreeLookState(PlayerStateMachine stateMachine, bool shouldFade = true) : base(stateMachine)
    {
        this.shouldFade = shouldFade;
    }

    public override void Enter()
    {
        //Subscribe das Target Event. Wenn die Taste TAB gedrückt wird switchen wir in den PlayerTargetState
        stateMachine.InputReader.TargetEvent += OnTarget;

        //Subscribe das Jump Event. Wenn die Taste Space gedrückt wird switchen wir in den PlayerJumpState
        stateMachine.InputReader.JumpEvent += OnJump;

        //Subscribe das PickUp Event. Wenn die Taste Space gedrückt wird switchen wir in den PlayerJumpState
        stateMachine.InputReader.PickUpEvent +=OnPickUp;


        stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0);

        //Starte FreeLook Animation, abhängig von shouldFade
        if (shouldFade) 
        {                                          
                stateMachine.Animator.CrossFadeInFixedTime(FreeLookTreeBlendHash, CrossFadeDuration);                     
        }
        else
        {
            stateMachine.Animator.Play(FreeLookTreeBlendHash);
        }
        
    }
    public override void Tick(float deltaTime)
    {
        //Wenn der Player den Attack Button (Left-Click, Button West) drückt
        //geht er vom FreeLook in den AttackState
        //Der zweite Parameter ist die Attacking ID, der Angriff beginnt mit der ersten Attacke in der AttackList
        //Array[0]
        if (stateMachine.InputReader.IsAttacking)
        {
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine,0));
            return;
        }

        Vector3 movement = CalculateMovement();

        //Move wird von der PlayerBaseState.cs aufgerufen
        //Movementspeed kann im Player-Inspector verändert werden
        Move(movement * stateMachine.FreeLookMovementSpeed, deltaTime);
        

        //Wenn der Wert 0 ist soll keine Berechnung durchgeführt werden
        //Der BlendTree FreeLookSpeed (Animator) wird smooth auf 0 gesetzt
        if (stateMachine.InputReader.MovementValue == Vector2.zero)
        {

            stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0, AnimatorDampTime, deltaTime);
            return;
        }

        //Der BlendTree FreeLookSpeed (Animator) wird smooth auf 1 gesetzt
        stateMachine.Animator.SetFloat(FreeLookSpeedHash, 1, AnimatorDampTime, deltaTime);

        FaceMovementDirection(movement,deltaTime);
    }

   

    public override void Exit()
    {
        stateMachine.InputReader.TargetEvent -= OnTarget;
        stateMachine.InputReader.JumpEvent -= OnJump;
        stateMachine.InputReader.PickUpEvent -= OnPickUp;
    }


    //Speichert die Vectoren in seperate Variablen
    //Setzt die y Komponente auf 0
    //Normalisiert beide Vectoren und setz die Größe auf 1
    //Gibt den Berechneten Vector3 für das Movement zurück
    private Vector3 CalculateMovement()
    {
        Vector3 forward = stateMachine.MainCameraTransform.forward;
        Vector3 right = stateMachine.MainCameraTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        return forward * stateMachine.InputReader.MovementValue.y
                + right*stateMachine.InputReader.MovementValue.x;
    }

    
    //Der Player dreht sich in die Richtung in die man gerade steuert
    //LERP Linere Inerpolation, um die Drehung smoother zu machen
    private void FaceMovementDirection(Vector3 movement, float deltaTime)
    {
            stateMachine.transform.rotation = Quaternion.Lerp(
            stateMachine.transform.rotation,
            Quaternion.LookRotation(movement),deltaTime*stateMachine.RotationSmoothValue);

       // Debug.Log(stateMachine.InputReader.MovementValue);
    }


    private void OnTarget()
    {
        //Nur wenn ein Target aus der Liste ausgewählt wurde (sie könnte ja leer sein) wechseln wir in den TargetModus
        //SelectTarget gibt ein bool zurück
        if (!stateMachine.Targeter.SelectTarget()) { return; }

        stateMachine.SwitchState(new PlayerTargetingSate(stateMachine));
    }


    private void OnJump()
    {
        stateMachine.SwitchState(new PlayerJumpingState(stateMachine));
    }

    private void OnPickUp()
    {
        if(!stateMachine.ItemTargeter.SelectStone()) 
        {
            return;
        }
        stateMachine.SwitchState(new PlayerPickUpState(stateMachine));
        
        //
        //if (stateMachine.ItemTargeter.SelectItem())
        //{
        //    stateMachine.SwitchState(new PlayerPickItemState(stateMachine));
        //}
    }
}
