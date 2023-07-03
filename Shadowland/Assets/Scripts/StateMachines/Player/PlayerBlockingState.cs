using UnityEngine;

public class PlayerBlockingState : PlayerBaseState
{
    private readonly int BlockHash = Animator.StringToHash("Block");
    private const float CrossFadeDuration = 0.1f;
    public PlayerBlockingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        //Player bekommt 80% weniger schaden ab
        stateMachine.Health.SetBlockingState(true);
        //Spiele die Block animation ab
        stateMachine.Animator.CrossFadeInFixedTime(BlockHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        if (!stateMachine.InputReader.IsBlocking)
        {
            if (stateMachine.Targeter.CurrentTarget == null)
            {
                stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
                return;
            }
            stateMachine.SwitchState(new PlayerTargetingSate(stateMachine));
            return;
        }

    }
    public override void Exit()
    {
        //Player bekommt wieder 100% schaden ab.
        stateMachine.Health.SetBlockingState(false);
    }

}
