using CUAS.MMT;
using UnityEngine;

public class PlayerDodgingState : PlayerBaseState
{

    private readonly int DodgeBlendTreeHash = Animator.StringToHash("DodgeBlendTree");
    private readonly int DodgeForwardTreeHash = Animator.StringToHash("DodgeForward");
    private readonly int DodgeRightTreeHash = Animator.StringToHash("DodgeRight");

    private float remainingDodgeTime;
    private Vector3 dodgingDirectionInput;

    private const float CrossFadeDuration = 0.1f;
    public PlayerDodgingState(PlayerStateMachine stateMachine, Vector3 dodgingDirectionInput) : base(stateMachine)
    {
        this.dodgingDirectionInput = dodgingDirectionInput;
    }

    public override void Enter()
    {
        remainingDodgeTime = stateMachine.DodgeDuration;

        //Animation BLend Tree für Dodging ansteuern
        stateMachine.Animator.SetFloat(DodgeForwardTreeHash, dodgingDirectionInput.y);
        stateMachine.Animator.SetFloat(DodgeRightTreeHash, dodgingDirectionInput.x);
        stateMachine.Animator.CrossFadeInFixedTime(DodgeBlendTreeHash, CrossFadeDuration);

        stateMachine.Health.SetInvulnerable(true);
        SoundManager.Instance.PlaySound(SoundManager.Sound.Player_Dodge);
    }

    public override void Tick(float deltaTime)
    {
        Vector3 movement = new Vector3();

        movement += stateMachine.transform.right * dodgingDirectionInput.x * stateMachine.DodgeDistance / stateMachine.DodgeDuration;
        movement += stateMachine.transform.forward * dodgingDirectionInput.y * stateMachine.DodgeDistance / stateMachine.DodgeDuration;

        Move(movement, deltaTime);
        FaceTarget();

        remainingDodgeTime -= deltaTime;

        if (remainingDodgeTime <= 0f)
        {
            stateMachine.SwitchState(new PlayerTargetingSate(stateMachine));
        }
    }
    public override void Exit()
    {
        stateMachine.Health.SetInvulnerable(false);
    }


}
