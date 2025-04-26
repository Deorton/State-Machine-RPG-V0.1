using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    [field: SerializeField] public CharacterController CharController { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public ForceReciever ForceReciever { get; private set; }
    [field: SerializeField] public WeaponHandler WeaponHandler { get; private set; }
    [field: SerializeField] public GameObject Player { get; private set; }
    [field: SerializeField] public float PlayerChasingRange { get; private set; }
    [field: SerializeField] public float CrossFadeDampTime { get; private set; }
    [field: SerializeField] public float AnimatorDampTime { get; private set; }

    void Awake()
    {
        CharController = GetComponent<CharacterController>();
        Animator = GetComponent<Animator>();
        ForceReciever = GetComponent<ForceReciever>();
        WeaponHandler = GetComponent<WeaponHandler>();

        Player = GameObject.FindGameObjectWithTag("Player");
        if (Player == null)
        {
            Debug.LogError("Player not found in the scene. Make sure the player has the 'Player' tag.");
        }
    }

    void Start()
    {
        SwitchState(new EnemyIdleState(this));
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, PlayerChasingRange);
    }
}
