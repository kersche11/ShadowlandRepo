using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField] private CharacterController characterController;

    private Collider[] allColliders;
    private Rigidbody[] allRigidBodys;

    //Wenn der Player oder das Target in den DeadState geht 
    //wird der Animator und CharacterController deaktiviert
    //Die Ragdoll Collider werden aktiviert und in den Rigidbodys die Gravity aktiviert
    //


    private void Start()
    {
        //True = alle Gameobjects die Inactive sind
        allColliders = GetComponentsInChildren<Collider>(true);
        allRigidBodys = GetComponentsInChildren<Rigidbody>(true);

        ToggleRagdoll(false);
    }

    public void ToggleRagdoll(bool isRagdoll)
    {
        foreach (Collider collider in allColliders)
        {
            if(collider.gameObject.CompareTag("Ragdoll"))
            {
                collider.enabled = isRagdoll;   
            }

        }

        foreach (Rigidbody rigidbody in allRigidBodys)
        {
            if (rigidbody.gameObject.CompareTag("Ragdoll"))
            {
                rigidbody.isKinematic = !isRagdoll;
                rigidbody.useGravity = isRagdoll;
            }
        }

        characterController.enabled = !isRagdoll;
        animator.enabled = !isRagdoll;
    }
}
