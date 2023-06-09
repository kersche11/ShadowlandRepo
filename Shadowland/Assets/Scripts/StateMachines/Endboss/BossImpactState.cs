using UnityEngine;

public class BossImpactState : BossBaseState
{
    private readonly int ImpactHash = Animator.StringToHash("Impact");
    private const float CrossFadeDuration = 0.1f;
    private float duration = 1f;
    public BossImpactState(BossStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(ImpactHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        duration -= deltaTime;
        if (duration <= 0f)
        {
            stateMachine.SwitchState(new BossIdleState(stateMachine));
            return;
        }
    }
    public override void Exit()
    {

    }


}
