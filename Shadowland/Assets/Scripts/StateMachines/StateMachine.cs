using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Die StateMachine Klasser ist abstract weil wir sie nicht im Game erzeugen wollen.
// Nur die Subklassen dieser Klasse werden verwendet
public abstract class StateMachine : MonoBehaviour
{
    private State currentState;
   
    private void Update()
    {
        //Das Fragezeichen ist gleich wie eine IF-Abfrage. 
        //Wir �berpr�fen ob currentState nicht "NULL" ist.
        currentState?.Tick(Time.deltaTime);
        
       
    }

    //Mit der SwitchState Methode verl�sst man den alten State und weist currentState
    //den neuen State zu. Dann Beginnt der neue currentState.
   public void SwitchState(State newState)
    {
        //Das Fragezeichen �berpr�ft ob currenstate nicht "NULL" ist.
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }
}
