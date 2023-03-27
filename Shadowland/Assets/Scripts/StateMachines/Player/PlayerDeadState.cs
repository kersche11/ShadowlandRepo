using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : PlayerBaseState
{
    public PlayerDeadState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        //Ragdoll
        Debug.Log("Death");
        stateMachine.Weapon.gameObject.SetActive(false);    

        //Respawn
    }
    public override void Tick(float deltaTime)
    {

    }
    public override void Exit()
    {
       
    }

   
}
