using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerImpactState : PlayerBaseState
{
    private readonly int ImpactHash = Animator.StringToHash("Impact");
    private readonly int BlockImpactHash = Animator.StringToHash("BlockImpact");
    private const float CrossFadeDuration = 0.1f;
    private float duration = 0.5f;

    public PlayerImpactState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        //Wenn der Player von eineme Enemy getroffen wird muss er den Stein fallen lassen
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            stateMachine.StoneCarryHandler?.SetStone();
            stateMachine.InputReader?.SetCarrying();
        }

        if (stateMachine.InputReader.IsBlocking)
        {
            stateMachine.Animator.CrossFadeInFixedTime(BlockImpactHash, CrossFadeDuration);
        }
        else
        {
            stateMachine.Animator.CrossFadeInFixedTime(ImpactHash, CrossFadeDuration);
        }

    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        duration -= deltaTime;

        if (duration <= 0f)
        {
            //Wenn Target geh in den TargetState, sonst in den FreeLookState
            ReturnToLocomation();
        }


    }
    public override void Exit()
    {

    }


}
