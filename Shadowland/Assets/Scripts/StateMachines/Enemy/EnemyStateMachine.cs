using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : StateMachine
{

    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public CharacterController EnemyController { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public NavMeshAgent navMeshAgent { get; private set; }
    [field: SerializeField] public WeaponDamage Weapon { get; private set; }
    [field: SerializeField] public float MovementSpeed { get; private set; }
    [field: SerializeField] public float PlayerChasingRange { get; private set; }

    [field: SerializeField] public float AttackRange { get; private set; }
    [field: SerializeField] public int AttackDamage { get; private set; }

    public GameObject Player { get; private set; }

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        //navMeshAgent Position und Rotationsupdate deaktivieren
        navMeshAgent.updatePosition = false;
        navMeshAgent.updateRotation = false;

        SwitchState(new EnemyIdleState(this));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, PlayerChasingRange);
    }

}
