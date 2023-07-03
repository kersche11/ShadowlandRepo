using System;
using UnityEngine;
using UnityEngine.InputSystem;

//Der InputReader erbt von MonoBehaviour und Controls
//Das ist nur m�glich weil Controls.IPlayingACtions ein INTERFACE ist!
//In dieser Klasse werden alle Actions des Players implementiert.
public class InputReader : MonoBehaviour, Controls.IPlayingActions, Controls.IMenuActions
{
    public bool IsAttacking { get; private set; }
    public bool IsBlocking { get; private set; }
    public bool IsCarrying { get; private set; }
    public Vector2 MovementValue { get; private set; }
    //Events
    public event Action JumpEvent;
    public event Action DodgeEvent;
    public event Action TargetEvent;
    public event Action PickUpEvent;


    private Controls controls;
    private void Start()
    {
        IsCarrying = false;
        //Wir brauchen eine Referenz der Callbacks zu dieser Klasse:
        controls = new Controls();
        controls.Playing.SetCallbacks(this);
        //Aktiviere die Controlls f�r Playing
        controls.Playing.Enable();
    }
    //OnDestroy wird ben�tigt wenn wir das Spiel unterbrechen
    private void OnDestroy()
    {
        //Deaktiviere die Controlls f�r Playing
        controls?.Playing.Disable();
    }
    //Springen mit Space oder Gamepad South
    public void OnJump(InputAction.CallbackContext context)
    {
        //performed hei�t die Taste ist gedr�ckt
        if (!context.performed) { return; }

        JumpEvent?.Invoke();
    }

    public void OnDodge(InputAction.CallbackContext context)
    {
        //performed = Taste ist gedr�ckt
        if (!context.performed) { return; }

        DodgeEvent?.Invoke();
    }

    //F�r das Bewegen in Y und Z Richtung
    public void OnMove(InputAction.CallbackContext context)
    {
        //Hier lesen wrir einen Vector2 value ein.
        MovementValue = context.ReadValue<Vector2>();
    }

    //F�r die Drehung der Kamera mit Maus oder Joystick
    public void OnLook(InputAction.CallbackContext context)
    {

    }

    public void OnTarget(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        TargetEvent?.Invoke();
    }



    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsAttacking = true;
        }
        else if (context.canceled)
        {
            IsAttacking = false;
        }
    }

    public void OnBlock(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            IsBlocking = true;
        }

        else if (context.canceled)
        {
            IsBlocking = false;
        }
    }

    public void OnPickUp(InputAction.CallbackContext context)
    {
        //performed = Taste ist gedr�ckt
        if (!context.performed) { return; }
        PickUpEvent?.Invoke();
    }

    public void SetCarrying()
    {
        IsCarrying = !IsCarrying;
    }




    public void OnClick(InputAction.CallbackContext context)
    {


    }
}
