using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    //Hier kommt der Player Collider rein um nicht sich selbst schaden zuzuf�gen
    [SerializeField] private Collider myCollider;
  

    private List<Collider> alreadyCollidedWith = new List<Collider>();
    private int damage;
    private float knockback;

    //Sobald dieses Script aktiviert wird. L�schen wir alle Elemente aus der Liste
    //Bei einem Schlag kann es passieren das man ein Target 2x trifft.
    //Um zu vermeiden das wir einem Target 2x Schadenzuf�gen, speichern wir das getroffene Target
    //in der alreadyCollidedWith Liste
    //Sobald dieses Script aktiviert wird. L�schen wir alle Elemente aus der Liste
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

        //�berpr�fe ob das getroffene Target Health besitzt wenn ja,
        //DealDamage auf das getroffene Objekt
        if (other.TryGetComponent<Health>(out Health health)) 
        {
            health.DealDamage(damage);
            
        }

        //Wenn das getroffene Target einen ForceReseiver hat wird das Targert bei einem Hit zur�ckgeschleudert
        //Knockback kann am Player und am Target eingestellt werden (Inspector)
        if (other.TryGetComponent<ForceReceiver>(out ForceReceiver forceReceiver))
        {
            Vector3 forceDirection = (other.transform.position - myCollider.transform.position).normalized;
            forceReceiver.AddForce(forceDirection*knockback);
        }

    }

    public void SetAttack(int damage, float knockback)
    {
       this.damage = damage;
       this.knockback = knockback;
    }
}
