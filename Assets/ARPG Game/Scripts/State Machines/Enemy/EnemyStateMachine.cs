using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : StateMachine
{
    [field: SerializeField] public CharacterController CharController { get; private set; }
    [field: SerializeField] public  NavMeshAgent NavAgent { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public ForceReciever ForceReciever { get; private set; }
    [field: SerializeField] public WeaponHandler WeaponHandler { get; private set; }
    [field: SerializeField] public GameObject Player { get; private set; }
    [field: SerializeField] public float PlayerChasingRange { get; private set; }
    [field: SerializeField] public float PlayerAttackingRange { get; private set; }
    [field: SerializeField] public float Damage { get; private set; }
    [field: SerializeField] public float StunTime { get; private set; }
    [field: SerializeField] public float AttackKnockback { get; private set; }
    [field: SerializeField] public float ChaseMovementSpeed { get; private set; }
    [field: SerializeField] public float PatrolMovementSpeed { get; private set; }
    [field: SerializeField] public float CrossFadeDampTime { get; private set; }
    [field: SerializeField] public float AnimatorDampTime { get; private set; }

    public float rotationDampTime = 0.1f;

    void Awake()
    {
        CharController = GetComponent<CharacterController>();
        Animator = GetComponent<Animator>();
        ForceReciever = GetComponent<ForceReciever>();
        WeaponHandler = GetComponent<WeaponHandler>();
        NavAgent = GetComponent<NavMeshAgent>();
        Health = GetComponent<Health>();

        Player = GameObject.FindGameObjectWithTag("Player");
        if (Player == null)
        {
            Debug.LogError("Player not found in the scene. Make sure the player has the 'Player' tag.");
        }
    }

    void Start()
    {
        NavAgent.updatePosition = false;
        NavAgent.updateRotation = false;

        SwitchState(new EnemyIdleState(this));
    }

    void OnEnable()
    {
        Health.OnTakeDamage += OnTakeDamage;
    }

    void OnDisable()
    {
        Health.OnTakeDamage -= OnTakeDamage;
    }

    private void OnTakeDamage(float stunTime)
    {
        SwitchState(new EnemyImpactState(this, stunTime));
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, PlayerChasingRange);
    }
}
