using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    //Hier kommt der Player Collider rein um nicht sich selbst schaden zuzufügen
    [SerializeField] private Collider myCollider;

    private List<Collider> alreadyCollidedWith = new List<Collider>();
    private int damage;

    //Sobald dieses Script aktiviert wird. Löschen wir alle Elemente aus der Liste
    private void OnEnable()
    {
        alreadyCollidedWith.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Ignoriere meine eigenen Collider
        if (other == myCollider) { return; }
      
        //Check ob das Target schon in der Liste ist, wann ja abbruch 
        if (alreadyCollidedWith.Contains(other)) { return; }

        //Wenn nicht, wir es jetzt geaddet
        alreadyCollidedWith.Add(other);

        //DealDamage auf das getroffene Objekt
        if (other.TryGetComponent<Health>(out Health health)) 
        {
            health.DealDamage(damage);
        }
    }

    public void SetAttack(int damage)
    {
       this.damage = damage;
    }
}
