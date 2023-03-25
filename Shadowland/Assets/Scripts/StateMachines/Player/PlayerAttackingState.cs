using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackingState : PlayerBaseState
{
    private float previousFrameTime;
    private bool alreadyAppliedForce;
    private Attack attack;
    public PlayerAttackingState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine)
    {
        attack=stateMachine.Attacks[attackIndex];
    }

    public override void Enter()
    {
        //Crossfade ist für die Transition von einer Animation zur nächsten
        //https://docs.unity3d.com/ScriptReference/Animator.CrossFadeInFixedTime.html
        stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName,attack.TransitionDuration);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        FaceTarget();

        float normalizedTime = GetNormallizedTime();

        if (normalizedTime >= previousFrameTime && normalizedTime < 1f)
        {

            //Forcetime kann einen Wert von 0-1 haben (0-100%)
            //Wir legen fest wie lange währen der Attacke, der Impact erhöht werden soll(Player Inspector).
            if (normalizedTime >= attack.ForceTime)
            {
                TryApplyForce();
            }


            if(stateMachine.InputReader.IsAttacking)
            {
                TryComboAttack(normalizedTime);
            }
        }

        //Nach der Attacke: Return zu einen Anderen Status
        else
        {
            //Wenn noch Targets in der nähe sind gehe in den TargetState
            if (stateMachine.Targeter.CurrentTarget!=null)
            {
                stateMachine.SwitchState(new PlayerTargetingSate(stateMachine));
            }
            else
            {
                stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            }
        }
        previousFrameTime = normalizedTime;

        Debug.Log(stateMachine.InputReader.IsAttacking);
        Debug.Log(attack.ComboStateIndex);
        
    }

   

    public override void Exit()
    {
       
    }

    private void TryComboAttack(float normalizedTime)
    {
        //Überprüfe ob wir überhaupt eine Comboattacke haben
        if (attack.ComboStateIndex == -1) { return; }

        if (normalizedTime<attack.ComboAttackTime) 
        {
            return; 
        }

        stateMachine.SwitchState
         (
            new PlayerAttackingState
            (
                stateMachine,attack.ComboStateIndex
            )

         );

    }

    private void TryApplyForce()
    {
        if (alreadyAppliedForce) { return; }
      
        stateMachine.ForceReceiver.AddForce(stateMachine.transform.forward * attack.Force);

        alreadyAppliedForce = true;
    }

    private float GetNormallizedTime()
    {
       AnimatorStateInfo currentInfo = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);
       AnimatorStateInfo nextInfo = stateMachine.Animator.GetNextAnimatorStateInfo(0);

        //Wenn eine Animation ausgeführt wird, die den Tag "Attack hat, schauen wir wie weit 
        //fortgeschritten diese Animation ist (Zeitlich)
        if (stateMachine.Animator.IsInTransition(0) && nextInfo.IsTag("Attack"))
        {
            return nextInfo.normalizedTime;
        }

        //Wenn keine Animation ausgeführt wird, die den Tag "Attack hat, schauen wir wie weit 
        //fortgeschritten die Animation ist (Zeitlich)
        else if (!stateMachine.Animator.IsInTransition(0) && currentInfo.IsTag("Attack"))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0f;
        }
    }

  
  
}
