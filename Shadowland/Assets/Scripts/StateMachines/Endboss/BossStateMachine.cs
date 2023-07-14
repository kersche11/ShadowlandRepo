using UnityEngine;
using UnityEngine.AI;

public class BossStateMachine : StateMachine
{
    [field: SerializeField] public WaypointPath WaypointPathPatroll { get; private set; }
    [field: SerializeField] public WaypointPath WaypointPathLanding { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public CharacterController EnemyController { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public NavMeshAgent navMeshAgent { get; private set; }
    [field: SerializeField] public WeaponDamage Weapon { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public Target Target { get; private set; }
    [field: SerializeField] public float MovementSpeed { get; private set; }
    [field: SerializeField] public float PlayerChasingRange { get; private set; }
    [field: SerializeField] public GameObject WaypointCenter { get; private set; }
    [field: SerializeField] public float AttackRange { get; private set; }
    [field: SerializeField] public int AttackDamage { get; private set; }
    [field: SerializeField] public int AttackKnockback { get; private set; }
    [field: SerializeField] public StartFightTrigger startFightTrigger { get; private set; }


    public Health Player { get; private set; }

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();

        //navMeshAgent Position und Rotationsupdate deaktivieren
        navMeshAgent.updatePosition = false;
        navMeshAgent.updateRotation = false;

        SwitchState(new BossPatrollState(this));
    }


    //Wenn dieses Script aktiv ist subscribe das event OnTakeDamage im Health.cs script
    private void OnEnable()
    {
        Health.OnTakeDamage += HandleTakeDamage;
        Health.OnDie += HandleDie;
    }

    private void OnDisable()
    {
        Health.OnTakeDamage -= HandleTakeDamage;
        Health.OnDie -= HandleDie;
    }
    private void HandleTakeDamage()
    {
        SwitchState(new BossImpactState(this));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, PlayerChasingRange);
    }
    private void HandleDie()
    {
        SwitchState(new BossDeadState(this));
    }

    public int GetTotalWaypoints()
    {
        // Implementieren Sie den Code, um die Gesamtanzahl der Wegpunkte zurückzugeben
        return WaypointPathLanding.transform.childCount; // Annahme: Die Wegpunkte sind als Kinder des WaypointPath-Objekts platziert
    }


}
