using CUAS.MMT;
using UnityEngine;

public class PlayerPickUpState : PlayerBaseState
{
    public Stone CurrentStone { get; private set; }



    //Speichert die Target Animation als Hash in den integer
    private readonly int LiftingObjectDownHash = Animator.StringToHash("LiftingObjectDown");
    private readonly int LiftingObjectUpHash = Animator.StringToHash("LiftingObjectUp");
    //private readonly int LiftingObjectUpHash = Animator.StringToHash("LiftingObjectUp");

    private const float CrossFadeDuration = 0.1f;



    public PlayerPickUpState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        CurrentStone = stateMachine.ItemTargeter.GetCurrentStone();
        stateMachine.StoneCarryHandler.SetCurrentStone(CurrentStone);

        SoundManager.Instance.PlaySound(SoundManager.Sound.Player_PickUpStone);
        if (stateMachine.InputReader.IsCarrying)
        {
            stateMachine.Animator.CrossFadeInFixedTime(LiftingObjectDownHash, CrossFadeDuration);
        }
        else
        {
            stateMachine.Animator.CrossFadeInFixedTime(LiftingObjectUpHash, CrossFadeDuration);
        }



    }

    public override void Tick(float deltaTime)
    {

        //Erst wenn die PickUp Animation fertig ist gehen wir in den FreeLookState 

        if (GetNormallizedTime(stateMachine.Animator, "LiftingObject") < 1) { return; }
        else
        {
            if (stateMachine.InputReader.IsCarrying)
            {
                stateMachine.InputReader.SetCarrying();
                stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            }
            else
            {
                stateMachine.InputReader.SetCarrying();
                stateMachine.SwitchState(new PlayerCarryState(stateMachine));
            }


        }
    }

    // Once the animations have finished, switch to the new state


    public override void Exit()
    {

    }



}
