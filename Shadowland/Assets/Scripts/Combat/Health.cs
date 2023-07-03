using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    [field: SerializeField] public Healthbar? HealthBar { get; private set; }

    public float currentHealth { get; private set; }
    private bool isBlocking;
    private bool isInvulnerable;

    //Return true wenn Player oder Target tot ist
    public bool IsDead => currentHealth == 0;

    public event Action OnTakeDamage;
    public event Action OnDie;

    void Start()
    {
        isBlocking = false;
        isInvulnerable = false;
        currentHealth = maxHealth;
        HealthBar?.SetMaxHealth(currentHealth);
    }

    public void SetBlockingState(bool isBlocking)
    {
        this.isBlocking = isBlocking;
    }

    public void SetInvulnerable(bool isInvulnerable)
    {
        this.isInvulnerable = isInvulnerable;
    }

    public void DealDamage(float damage)
    {
        if (currentHealth == 0) { return; }

        //Wenn der Player Blockt bekommt er 80% weniger schaden.
        if (isBlocking)
        {
            damage *= 0.2f;
        }

        if (isInvulnerable)
        {
            damage = 0;
        }

        currentHealth -= damage;

        if (currentHealth < 0) { currentHealth = 0; }

        //TakeDamageEvent um in den ImpactState zu wechseln
        OnTakeDamage?.Invoke();

        if (currentHealth == 0)
        {
            OnDie?.Invoke();
        }
        HealthBar?.SetHealth(currentHealth);
        Debug.Log("Dealed Damage: " + damage);
    }

    public void ResetHealth(int health)
    {
        currentHealth = health;
    }

    public float GetHealth()
    {
        return currentHealth;
    }
}
