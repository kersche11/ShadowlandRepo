using UnityEngine;

public class BossDeadState : BossBaseState
{
    public BossDeadState(BossStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        //stateMachine.Ragdoll.ToggleRagdoll(true);
        stateMachine.Weapon.gameObject.SetActive(false);
        GameObject.Destroy(stateMachine.Target);

    }
    public override void Tick(float deltaTime)
    {

    }
    public override void Exit()
    {

    }

}
