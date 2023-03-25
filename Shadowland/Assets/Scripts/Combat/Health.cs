using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    public int currentHealth {get; private set;}
    void Start()
    {
        currentHealth = maxHealth; 
    }

    public void DealDamage(int damage)
    {
        if (currentHealth == 0) { return; }
        
        currentHealth -= damage;
        
        if (currentHealth < 0) { currentHealth = 0; }
       // Debug.Log(currentHealth.ToString());
    }
}
