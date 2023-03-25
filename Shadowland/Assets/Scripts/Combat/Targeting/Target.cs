using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public event Action<Target> DestroyEvent;

    //Immer wenn ein Target zerstört wird, triggern wir das DestroyEvent, um 
    //Das Target aus der TargetingGoup List zu löschen.
    private void OnDestroy()
    {
        DestroyEvent?.Invoke(this);
    }
}
