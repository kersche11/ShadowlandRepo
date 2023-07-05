using CUAS.MMT;
using UnityEngine;

public class PlayerFallingState : PlayerBaseState
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

        stateMachine.LedgeDetector.OnLedgeDetect += HandleLedgeDetection;

    }

    public override void Tick(float deltaTime)
    {

        Vector3 movement = CalculateMovement();
        movement += momentum;
        Move(movement, deltaTime);

        //Wenn der Player am Boden landet kehrt er in den FreelookState oder TargetState zurück.
        if (stateMachine.CharacterController.isGrounded)
        {
            ReturnToLocomation();
        }

        FaceTarget();
    }

    public override void Exit()
    {
        SoundManager.Instance.PlaySound(SoundManager.Sound.Player_Landing_Jump);
        stateMachine.LedgeDetector.OnLedgeDetect -= HandleLedgeDetection;
    }

    private void HandleLedgeDetection(Vector3 ledgeForward, Vector3 closetPoint)
    {
        stateMachine.SwitchState(new PlayerHangingState(stateMachine, ledgeForward, closetPoint));
    }

    private Vector3 CalculateMovement()
    {
        Vector3 forward = stateMachine.MainCameraTransform.forward;
        Vector3 right = stateMachine.MainCameraTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Vector3 momentum = forward * stateMachine.InputReader.MovementValue.y
                  + right * stateMachine.InputReader.MovementValue.x;


        //momentum += stateMachine.CharacterController.velocity;
        //momentum.y = 0f;


        return momentum;
    }

}
