using CUAS.MMT;
using UnityEngine;

public class PlayerCarryState : PlayerBaseState
{
    private readonly int CarryingBlendHash = Animator.StringToHash("CarryingBlendTree");
    private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");
    private const float AnimatorDampTime = 0.1f;
    private const float CrossFadeDuration = 0.1f;
    float FLmoveSpeed;
    public PlayerCarryState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        //Subscribe das PickUp Event. Wenn die Taste Space gedrückt wird switchen wir in den PlayerJumpState
        stateMachine.InputReader.PickUpEvent += OnPickUp;
        stateMachine.Animator.CrossFadeInFixedTime(CarryingBlendHash, CrossFadeDuration);
        FLmoveSpeed = stateMachine.FreeLookMovementSpeed;
        stateMachine.SetMovementSpeed(4);
        stateMachine.PlayerAudio.clip = SoundManager.Instance.GetClip(SoundManager.Sound.Player_CarryStone);
        
    }

    public override void Tick(float deltaTime)
    {

        Vector3 movement = CalculateMovement();

        //Move wird von der PlayerBaseState.cs aufgerufen
        //Movementspeed kann im Player-Inspector verändert werden
        Move(movement * stateMachine.FreeLookMovementSpeed, deltaTime);

        //Wenn der Wert 0 ist soll keine Berechnung durchgeführt werden
        //Der BlendTree FreeLookSpeed (Animator) wird smooth auf 0 gesetzt
        if (stateMachine.InputReader.MovementValue == Vector2.zero)
        {
            stateMachine.PlayerAudio.enabled = false;
            stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0, AnimatorDampTime, deltaTime);
            return;
        }
        else
        {
            stateMachine.PlayerAudio.enabled = true;
        }

        //Der BlendTree FreeLookSpeed (Animator) wird smooth auf 1 gesetzt
        stateMachine.Animator.SetFloat(FreeLookSpeedHash, 1, AnimatorDampTime, deltaTime);

        FaceMovementDirection(movement, deltaTime);
    }

    public override void Exit()
    {
        stateMachine.SetMovementSpeed(FLmoveSpeed);
        stateMachine.InputReader.PickUpEvent -= OnPickUp;
        stateMachine.PlayerAudio.clip = SoundManager.Instance.GetClip(SoundManager.Sound.Player_Run_Meadow);
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
                + right * stateMachine.InputReader.MovementValue.x;
    }

    //Der Player dreht sich in die Richtung in die man gerade steuert
    //LERP Linere Inerpolation, um die Drehung smoother zu machen
    private void FaceMovementDirection(Vector3 movement, float deltaTime)
    {
        stateMachine.transform.rotation = Quaternion.Lerp(
        stateMachine.transform.rotation,
        Quaternion.LookRotation(movement), deltaTime * stateMachine.RotationSmoothValue);

        // Debug.Log(stateMachine.InputReader.MovementValue);
    }

    private void OnPickUp()
    {
        stateMachine.SwitchState(new PlayerPickUpState(stateMachine));
    }

}
