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
    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine){}

    public override void Enter()
    {
        //Subscribe das Target Event. Wenn die Taste TAB gedr�ckt wird switchen wir in den PlayerTargetState
        stateMachine.InputReader.TargetEvent += OnTarget;

        //Subscribe das Target Event. Wenn die Taste Space gedr�ckt wird switchen wir in den PlayerJumpState
        stateMachine.InputReader.JumpEvent += OnJump;

        //Starte FreeLook Animation
        stateMachine.Animator.Play(FreeLookTreeBlendHash);
    }
    public override void Tick(float deltaTime)
    {
        Vector3 movement = CalculateMovement();

        //Move wird von der PlayerBaseState.cs aufgerufen
        //Movementspeed kann im Player-Inspector ver�ndert werden
        Move(movement * stateMachine.FreeLookMovementSpeed, deltaTime);
        

        //Wenn der Wert 0 ist soll keine Berechnung durchgef�hrt werden
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
    }


    //Speichert die Vectoren in seperate Variablen
    //Setzt die y Komponente auf 0
    //Normalisiert beide Vectoren und setz die Gr��e auf 1
    //Gibt den Berechneten Vector3 f�r das Movement zur�ck
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

        Debug.Log(stateMachine.InputReader.MovementValue);
    }


    private void OnTarget()
    {
        //Nur wenn ein Target aus der Liste ausgew�hlt wurde (sie k�nnte ja leer sein) wechseln wir in den TargetModus
        //SelectTarget gibt ein bool zur�ck
        if (!stateMachine.Targeter.SelectTarget()) { return; }

        stateMachine.SwitchState(new PlayerTargetingSate(stateMachine));
    }


    private void OnJump()
    {
        stateMachine.SwitchState(new PlayerJumpingState(stateMachine));
    }


}