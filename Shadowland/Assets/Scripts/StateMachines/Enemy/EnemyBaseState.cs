using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState : State
{
    protected EnemyStateMachine stateMachine;
   

    public EnemyBaseState(EnemyStateMachine stateMachine) 
    {
        this.stateMachine = stateMachine;
    }


    //Move für IdleState
    protected void Move(Vector3 motion, float deltaTime)
    {

        stateMachine.EnemyController.Move((motion + stateMachine.ForceReceiver.Movement) * deltaTime);
    }

    //Move für AttackState (ohne motion)
    protected void Move(float deltaTime)
    {

        Move(Vector3.zero, deltaTime);
    }


    //Wenn der Gegner den Player verfolgt dreht er sich richtung Player
    protected void FacePlayer()
    {
        if (stateMachine.Player == null) { return; }

        Vector3 lookPositon = stateMachine.Player.transform.position - stateMachine.transform.position;

        lookPositon.y = 0f;

        stateMachine.transform.rotation = Quaternion.LookRotation(lookPositon);
    }



    //Errechnet ob der Player in Reichweite des Gegener ist
    //Wenn die Distanz kleiner ist gibt geben wir ein True zurück
    protected bool IsInChaseRange()
    {

       float distanceToPlayer= (stateMachine.Player.transform.position - stateMachine.transform.position).sqrMagnitude;

        return distanceToPlayer <= stateMachine.PlayerChasingRange * stateMachine.PlayerChasingRange;
    }
   
}
