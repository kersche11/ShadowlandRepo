using UnityEngine;

public class BossDeadState : BossBaseState
{
    private readonly int AttackHash = Animator.StringToHash("Death");
    public BossDeadState(BossStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("Enter Death State");
        //stateMachine.Ragdoll.ToggleRagdoll(true);
        stateMachine.Weapon.gameObject.SetActive(false);
        GameObject.Destroy(stateMachine.Target);
        stateMachine.Animator.Play(AttackHash);

    }
    public override void Tick(float deltaTime)
    {

    }
    public override void Exit()
    {

    }

}
