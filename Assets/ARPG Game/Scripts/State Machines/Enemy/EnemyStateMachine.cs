using System.Collections;
using System.Collections.Generic;
using ARPG.Attibutes;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : StateMachine
{
    [field: Header("Enemy Components")]
    [field: SerializeField] public CharacterController CharController { get; private set; }
    [field: SerializeField] public  NavMeshAgent NavAgent { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public ForceReciever ForceReciever { get; private set; }
    [field: SerializeField] public WeaponHandler WeaponHandler { get; private set; }
    [field: SerializeField] public Target Target { get; private set; }
    [field: SerializeField] public Health Player { get; private set; }
    [field: SerializeField] public Ragdoll Ragdoll { get; private set; }
    [field: SerializeField] public PatrolPath PatrolPath { get; private set; }

    [field: Header("Enemy Ranges")]
    [field: SerializeField] public float PlayerChasingRange { get; private set; }
    [field: SerializeField] public float PlayerAttackingRange { get; private set; }

    [field: Header("Enemy Attack Stats")]
    [field: SerializeField] public float Damage { get; private set; }
    [field: SerializeField] public float StunTime { get; private set; }
    [field: SerializeField] public float AttackKnockback { get; private set; }

    [field: Header("Enemy Movement Speeds")]
    [field: SerializeField] public float ChaseMovementSpeed { get; private set; }
    [field: SerializeField] public float PatrolMovementSpeed { get; private set; }

    [field: Header("Enemy Animation Times")]
    [field: SerializeField] public float CrossFadeDampTime { get; private set; }
    [field: SerializeField] public float AnimatorDampTime { get; private set; }

    [field: Header("Enemy Ability Settings")]
    [field: SerializeField] public bool IsGaurding { get; private set; }
    [field: SerializeField] public bool CanPatrol { get; private set; }
    [field: SerializeField] public float SuspicionTime { get; private set; }
    [field: SerializeField] public float WaypointDwellTime { get; private set; }

    public int _currentWaypointIndex = 0;
    public Vector3 _guardPosition;
    public Vector3 _guardRotation;

    public float rotationDampTime = 0.1f;

    void Awake()
    {
        CharController = GetComponent<CharacterController>();
        Animator = GetComponent<Animator>();
        ForceReciever = GetComponent<ForceReciever>();
        WeaponHandler = GetComponent<WeaponHandler>();
        NavAgent = GetComponent<NavMeshAgent>();
        Health = GetComponent<Health>();
        Target = GetComponent<Target>();
        Ragdoll = GetComponent<Ragdoll>();

        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        if (Player == null)
        {
            Debug.LogError("Player not found in the scene. Make sure the player has the 'Player' tag.");
        }

        _guardPosition = transform.position;
        _guardRotation = transform.eulerAngles;
    }

    void Start()
    {
        NavAgent.updatePosition = false;
        NavAgent.updateRotation = false;

        if (IsGaurding)
        {
            SwitchState(new EnemyGaurdState(this));
        }
        else if (CanPatrol)
        {
            SwitchState(new EnemyPatrolState(this));
        }
        else
        {
            SwitchState(new EnemyIdleState(this));
        }
    }

    void OnEnable()
    {
        Health.OnTakeDamage += OnTakeDamage;
        Health.OnDie += HandleDie;
    }

    void OnDisable()
    {
        Health.OnTakeDamage -= OnTakeDamage;
        Health.OnDie -= HandleDie;
    }

    private void OnTakeDamage(float stunTime)
    {
        SwitchState(new EnemyImpactState(this, stunTime));
    }

    private void HandleDie()
    {
        SwitchState(new EnemyDeadState(this));
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, PlayerChasingRange);
    }
}
