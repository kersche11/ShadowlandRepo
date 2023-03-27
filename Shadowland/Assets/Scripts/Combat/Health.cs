using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    public int currentHealth {get; private set;}

    public event Action OnTakeDamage;
    public event Action OnDie;

    void Start()
    {
        currentHealth = maxHealth; 
    }

    public void DealDamage(int damage)
    {
        if (currentHealth == 0) { return; }
        
        currentHealth -= damage;
        
        if (currentHealth < 0) { currentHealth = 0; }

        //TakeDamageEvent um in den ImpactState zu wechseln
        OnTakeDamage?.Invoke();
        
        if (currentHealth ==0)
        {
            OnDie?.Invoke();
        }
    }
}
