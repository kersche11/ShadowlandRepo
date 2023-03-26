using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    //Referenz zum Player
    [SerializeField] private CharacterController characterController;

    //Setting fpr Damping: Wie schnell soll der Impact wieder reduziert werden
    [SerializeField] private float impactDrag = 0.3f;

    private float verticalVelocity;
    private Vector3 impact;
    private Vector3 dampingVelocity;

    


    //Das Movement basiert auf auf der Gravitation und Externen Einflüssen zb: Schlag mit dem Schwert.
    public Vector3 Movement => impact+Vector3.up * verticalVelocity;

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
            verticalVelocity += Physics.gravity.y*Time.deltaTime;
        }


        //https://docs.unity3d.com/ScriptReference/Vector3.SmoothDamp.html
        //Hier wird der Impact wieder smooth auf 0 reduziert (pro frame)

        impact = Vector3.SmoothDamp(impact,Vector3.zero,ref dampingVelocity,impactDrag);
    }


    //Add impact zu movement
    public void AddForce(Vector3 force)
    {
        impact += force;
    }

    public void JumpForce(float jumpforce)
    {
        verticalVelocity += jumpforce;
    }
}
