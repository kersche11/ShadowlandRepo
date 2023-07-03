using UnityEngine;

public abstract class State
{

    public abstract void Enter();
    public abstract void Tick(float deltaTime);
    public abstract void Exit();



    //Diese Methode wird von Player und Enemy benutzt, deshalb ist sie in der BaseClass
    //Wir evaluieren wie lange eine Animation dauernt und Normalisieren die Zeit (0-1)
    protected float GetNormallizedTime(Animator animator, string tag)
    {
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(0);

        //Wenn eine Animation ausgeführt wird, die den Tag "Attack hat, schauen wir wie weit 
        //fortgeschritten diese Animation ist (Zeitlich)
        if (animator.IsInTransition(0) && nextInfo.IsTag(tag))
        {
            return nextInfo.normalizedTime;
        }

        //Wenn keine Animation ausgeführt wird, die den Tag "Attack hat, schauen wir wie weit 
        //fortgeschritten die Animation ist (Zeitlich)
        else if (!animator.IsInTransition(0) && currentInfo.IsTag(tag))
        {
            return currentInfo.normalizedTime;
        }
        else
        {
            return 0f;
        }
    }
}
