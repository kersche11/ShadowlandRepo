using UnityEngine;

public class EnemyAttackingState : EnemyBaseState
{

    private readonly int AttackHash = Animator.StringToHash("Attack1");

    private const float TransitionDuration = 0.1f;

    public EnemyAttackingState(EnemyStateMachine stateMachine) : base(stateMachine)
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
        //und führt die nächste Attack aus.
        if (GetNormallizedTime(stateMachine.Animator, "Attack") >= 1)
        {
            stateMachine.SwitchState(new EnemyChasingState(stateMachine));
        }
        FacePlayer();
    }
    public override void Exit()
    {

    }
}


