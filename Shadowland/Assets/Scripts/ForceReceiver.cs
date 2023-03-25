using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    //Referenz zum Player
    [SerializeField] private CharacterController characterController;

    private float verticalVelocity;

    public Vector3 Movement => Vector3.up * verticalVelocity;

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

        //Debug.Log(verticalVelocity.ToString());
    }
}
