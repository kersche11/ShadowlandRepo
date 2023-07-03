using UnityEngine;

public class PlayerPullUpState : PlayerBaseState
{

    private readonly int pullUpHash = Animator.StringToHash("PullUp");
    private readonly Vector3 Offset = new Vector3(0f, 2.325f, 0.65f);
    private const float CrossFadeDuration = 0.1f;

    public PlayerPullUpState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {

        stateMachine.Animator.CrossFadeInFixedTime(pullUpHash, CrossFadeDuration);

    }
    public override void Tick(float deltaTime)
    {
        //Erst wenn die PullUp Animation fertig ist gehen wir in den FreeLookState 
        if (GetNormallizedTime(stateMachine.Animator, "PullUp") < 1) { return; }

        //Verschiebe den Character and die Endposition der PullUp Animation
        stateMachine.CharacterController.enabled = false;
        stateMachine.transform.Translate(Offset, Space.Self);
        stateMachine.CharacterController.enabled = true;

        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine, false));
    }
    public override void Exit()
    {
        stateMachine.CharacterController.Move(Vector3.zero);
        stateMachine.ForceReceiver.Reset();
    }


}
