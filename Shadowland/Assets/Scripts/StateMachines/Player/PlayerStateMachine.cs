using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The PlayerStateMachine erbt das MonoBehaviour �ber die StateMachine Klasse.
//MonoBehaviour erlaubt uns das Script auf ein Gameobject zuzuweisen.
public class PlayerStateMachine : StateMachine
{
    //Property. Hier darf man den InoutReader nur lesen aber nicht schreiben
    //Unity Serialisiert von alleine (public) keine Properties nur Fields! => [field:SerializeField]
    //Ohne [field:SerializeField] wird im Inspector nichts angezeigt

    [field: SerializeField] public InputReader InputReader{get; private set;}

    //Den Character Controller braucht man um kollisonen zu handeln 
    [field: SerializeField] public CharacterController CharacterController { get; private set; }

    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public Targeter Targeter { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }

    //[field: SerializeField] public float WalkingMovementSpeed { get; private set; }
    [field: SerializeField] public float FreeLookMovementSpeed { get; private set; }
    [field: SerializeField] public float TargetingMovementSpeed { get; private set; }
    

    [field: SerializeField] public float RotationSmoothValue { get; private set; }
    [field: SerializeField] public float JumpForce { get; private set; }


   

    //Um den Player in Abh�ngigkeit der Kamera zu steuern (relative to Camera) brauchen wir die Transform der MainCamera
    public Transform MainCameraTransform { get; private set; }

    private void Start()
    {

        MainCameraTransform = Camera.main.transform;

        //"This" referenziert auf die Instanz in der wir uns gerade befinden.
        SwitchState(new PlayerFreeLookState(this)); 
    }

 
}