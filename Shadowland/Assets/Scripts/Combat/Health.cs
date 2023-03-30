using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    public float currentHealth {get; private set;}
    private bool isBlocking;

    //Return true wenn Player oder Target tot ist
    public bool IsDead => currentHealth == 0;

    public event Action OnTakeDamage;
    public event Action OnDie;

    void Start()
    {
        currentHealth = maxHealth; 
    }

    public void SetBlockingState(bool isBlocking)
    {
        this.isBlocking = isBlocking;
    }

    public void DealDamage(float damage)
    {
        if (currentHealth == 0) { return; }

        //Wenn der Player Blockt bekommt er 80% weniger schaden.
        if (isBlocking) 
        {
            damage *= 0.2f;
        }
        
        currentHealth -= damage;
        
        if (currentHealth < 0) { currentHealth = 0; }

        //TakeDamageEvent um in den ImpactState zu wechseln
        OnTakeDamage?.Invoke();
        
        if (currentHealth ==0)
        {
            OnDie?.Invoke();
        }
        Debug.Log("Dealed Damage: " + damage);
    }

    public void ResetHealth(int health)
    {
        currentHealth = health;
    }
}
