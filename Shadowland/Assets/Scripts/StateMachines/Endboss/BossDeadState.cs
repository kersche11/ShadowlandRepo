using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;
using static SceneList;

public class BossDeadState : BossBaseState
{
    private readonly int AttackHash = Animator.StringToHash("Death");

    private float elapsedTime = 0f;
    private float transitionTime = 10f;


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

        elapsedTime = 0f;

    }
    public override void Tick(float deltaTime)
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= transitionTime)
        {
            SceneManager.LoadScene("EndSzene");

        }

    }
    public override void Exit()
    {

    }

   


}
