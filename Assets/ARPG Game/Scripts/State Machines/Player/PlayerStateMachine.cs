using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field: SerializeField] public CharacterController PlayerController { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public InputReader InputReader { get; private set; }
    [field: SerializeField] public Targeter Targeter { get; private set; }
    [field: SerializeField] public ForceReciever ForceReciever { get; private set; }
    [field: SerializeField] public WeaponHandler WeaponHandler { get; private set; }
    [field: SerializeField] public AttackData[] AttackDatas { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }

    [field: SerializeField] public float FreeLookMovementSpeed { get; private set; }
    
    [field: SerializeField] public float StunTime { get; private set; }
    [field: SerializeField] public float TargetingMovementSpeed { get; private set; }
    [field: SerializeField] public float CrossFadeDampTime { get; private set; }

    public Transform MainCameraTransform { get; private set; }
    public float animatorDampTime = 0.1f;
    public float rotationDampTime = 0.1f;

    void Awake()
    {
        PlayerController = GetComponent<CharacterController>();
        Animator = GetComponent<Animator>();
        InputReader = GetComponent<InputReader>();
        Targeter = GetComponentInChildren<Targeter>();
        ForceReciever = GetComponent<ForceReciever>();
        MainCameraTransform = Camera.main.transform;
        WeaponHandler = GetComponent<WeaponHandler>();
        Health = GetComponent<Health>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SwitchState(new PlayerFreeLookState(this));
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
        SwitchState(new PlayerImpactState(this, stunTime));
    }
}
