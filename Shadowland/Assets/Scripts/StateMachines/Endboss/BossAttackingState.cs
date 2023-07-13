using UnityEngine;

public class BossAttackingState : BossBaseState
{

    private readonly int AttackHash = Animator.StringToHash("Attack");

    private const float TransitionDuration = 0.1f;

    public BossAttackingState(BossStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {

        stateMachine.Weapon.SetAttack(stateMachine.AttackDamage, stateMachine.AttackKnockback);
        stateMachine.Animator.CrossFadeInFixedTime(AttackHash, TransitionDuration);
    }

    public override void Tick(float deltaTime)
    {
        //Nach der AttackAnimation gehen wir in den ChasingState
        //Sollte der Player noch in der AttackingRange sein geht der Gegner sofort wieder in den Attackstate
        //und f�hrt die n�chste Attack aus.
        if (GetNormallizedTime(stateMachine.Animator, "Attack") >= 1)
        {
            stateMachine.SwitchState(new BossChasingWalkState(stateMachine));
        }
        FacePlayer();
    }
    public override void Exit()
    {

    }
}


