using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Der InputReader erbt von MonoBehaviour und Controls
//Das ist nur möglich weil Controls.IPlayingACtions ein INTERFACE ist!
//In dieser Klasse werden alle Actions des Players implementiert.
public class InputReader : MonoBehaviour, Controls.IPlayingActions
{

    public Vector2 MovementValue {  get; private set; }
    //Events
    public event Action JumpEvent;
    public event Action DodgeEvent;
    public event Action TargetEvent;
    public event Action CancelEvent;

    private Controls controls;
    private void Start()
    {
        //Wir brauchen eine Referenz der Callbacks zu dieser Klasse:
        controls = new Controls();
        controls.Playing.SetCallbacks(this);
        //Aktiviere die Controlls für Playing
        controls.Playing.Enable();
    }
    //OnDestroy wird benötigt wenn wir das Spiel unterbrechen
    private void OnDestroy()
    {
        //Deaktiviere die Controlls für Playing
        controls.Playing.Disable();
    }
    //Springen mit Space oder Gamepad South
    public void OnJump(InputAction.CallbackContext context)
    {
        //performed heißt die Taste ist gedrückt
        if(!context.performed) { return; }

        JumpEvent?.Invoke();
    }

    public void OnDodge(InputAction.CallbackContext context)
    {
        //performed = Taste ist gedrückt
        if (!context.performed) { return; }

        DodgeEvent?.Invoke();
    }

    //Für das Bewegen in Y und Z Richtung
    public void OnMove(InputAction.CallbackContext context)
    {
        //Hier lesen wrir einen Vector2 value ein.
        MovementValue=context.ReadValue<Vector2>();
    }

    //Für die Drehung der Kamera mit Maus oder Joystick
    public void OnLook(InputAction.CallbackContext context)
    {
        
    }

    public void OnTarget(InputAction.CallbackContext context)
    {
        if (!context.performed) { return ; }
        TargetEvent?.Invoke();  
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }
        CancelEvent?.Invoke();
    }
}
