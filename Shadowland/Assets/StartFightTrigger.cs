using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFightTrigger : MonoBehaviour
{
    public event Action FightEvent;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            {
            FightEvent?.Invoke(); 
            }
    }
}
