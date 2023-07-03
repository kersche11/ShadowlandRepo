using UnityEngine;
using UnityEngine.AI;

public class ForceReceiver : MonoBehaviour
{
    //Referenz zum Player
    [SerializeField] private CharacterController characterController;
    //Referenz zum Target
    [SerializeField] private NavMeshAgent navMeshAgent;

    //Setting fpr Damping: Wie schnell soll der Impact wieder reduziert werden
    [SerializeField] private float impactDrag = 0.3f;

    private float verticalVelocity;
    private Vector3 impact;
    private Vector3 dampingVelocity;




    //Das Movement basiert auf auf der Gravitation und Externen Einflüssen zb: Schlag mit dem Schwert.
    public Vector3 Movement => impact + Vector3.up * verticalVelocity;

    //Kalkuliere Velocity jeden Frame 
    private void Update()
    {
        if (verticalVelocity < 0f && characterController.isGrounded)
        {
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            //Gravety ist negativ, deshalb geht man hinunter wenn man addiert
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }


        //https://docs.unity3d.com/ScriptReference/Vector3.SmoothDamp.html
        //Hier wird der Impact wieder smooth auf 0 reduziert (pro frame)
        impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity, impactDrag);

        if (navMeshAgent != null)
        {
            if (impact.sqrMagnitude < 0.2f * 0.2f)
            {
                navMeshAgent.enabled = true;
            }
        }

    }


    //Add impact zu movement
    public void AddForce(Vector3 force)
    {
        impact += force;

        //Bei einem Knockback wird der NavMeshAgent deaktiviert
        if (navMeshAgent != null)
        {
            navMeshAgent.enabled = false;
        }
    }

    public void JumpForce(float jumpforce)
    {
        verticalVelocity += jumpforce;


    }

    public void Reset()
    {
        impact = Vector3.zero;
        verticalVelocity = 0f;
    }
}
