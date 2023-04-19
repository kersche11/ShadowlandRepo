using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeadState : PlayerBaseState
{

    private float countdown=10;
    public PlayerDeadState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        //Ragdoll
        stateMachine.Ragdoll.ToggleRagdoll(true);
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            stateMachine.StoneCarryHandler?.SetStone();
        }
        stateMachine.Weapon.gameObject.SetActive(false);


        

        //Respawn
    }
    public override void Tick(float deltaTime)
    {
       countdown -= deltaTime;
        if (countdown <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    public override void Exit()
    {
       
    }

}
