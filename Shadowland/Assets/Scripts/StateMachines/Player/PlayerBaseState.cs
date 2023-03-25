using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//PlayerBaseState muss abstract sein weil State auch abstract ist.
//Ansonsten muss man hier alle Methoden von State implementieren.
public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine stateMachine;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        //Referenz zur PlayerStateMachine
        this.stateMachine = stateMachine;   
    }

    //Move für FreeLookState und TargetState
    protected void Move(Vector3 motion, float deltaTime)
    {
       
        stateMachine.CharacterController.Move((motion + stateMachine.ForceReceiver.Movement) * deltaTime);
    }

    //Move für AttackState (ohne motion)
    protected void Move(float deltaTime)
    {

        Move(Vector3.zero,deltaTime);
    }

    //Im PlayerTargetState wollen wir immer Richtung Target schauen
    //Wenn wir ein aktuelles Target haben errechnen wir uns die Richtung
    //indem wir die postion des Targets minus der player postion rechnen.
    //Y wird auf 0 gesetzt da wir nur die links, rechts schauen wollen, die höhe ist egal.
    //Für die Rotation wandeln wir den Vector on Quaternione um.
    protected void FaceTarget()
    {
        if(stateMachine.Targeter.CurrentTarget == null) { return; }

        Vector3 lookPositon = stateMachine.Targeter.CurrentTarget.transform.position-stateMachine.transform.position;

        lookPositon.y = 0f;

        stateMachine.transform.rotation = Quaternion.LookRotation(lookPositon);
    }

}
