using System.Collections.Generic;
using UnityEngine;

public class TrapDamage : MonoBehaviour
{    //Hier kommt der Player Collider rein um nicht sich selbst schaden zuzufügen
    [SerializeField] private Collider myCollider;
    [SerializeField] private int damage;
    [SerializeField] private float knockback;

    private List<Collider> alreadyCollidedWith = new List<Collider>();


    //Sobald dieses Script aktiviert wird. Löschen wir alle Elemente aus der Liste
    //Bei einem Schlag kann es passieren das man ein Target 2x trifft.
    //Um zu vermeiden das wir einem Target 2x Schadenzufügen, speichern wir das getroffene Target
    //in der alreadyCollidedWith Liste
    //Sobald dieses Script aktiviert wird. Löschen wir alle Elemente aus der Liste
    private void OnEnable()
    {
        alreadyCollidedWith.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Ignoriere meine eigenen Collider
        if (other == myCollider) { return; }



        //Überprüfe ob das getroffene Target Health besitzt wenn ja,
        //DealDamage auf das getroffene Objekt
        if (other.TryGetComponent<Health>(out Health health))
        {
            health.DealDamage(damage);

        }

        //Wenn das getroffene Target einen ForceReseiver hat wird das Targert bei einem Hit zurückgeschleudert
        //Knockback kann am Player und am Target eingestellt werden (Inspector)
        if (other.TryGetComponent<ForceReceiver>(out ForceReceiver forceReceiver))
        {
            Vector3 forceDirection = (other.transform.position - myCollider.transform.position).normalized;
            forceReceiver.AddForce(forceDirection * knockback);
        }

    }

    //public void SetAttack(int damage, float knockback)
    //{
    //    this.damage = damage;
    //    this.knockback = knockback;
    //}
}
