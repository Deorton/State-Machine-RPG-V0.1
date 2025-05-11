using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field: Header("References")]
    [field: SerializeField] public CharacterController PlayerController { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public InputReader InputReader { get; private set; }
    [field: SerializeField] public Targeter Targeter { get; private set; }
    [field: SerializeField] public ForceReciever ForceReciever { get; private set; }
    [field: SerializeField] public WeaponHandler WeaponHandler { get; private set; }
    [field: SerializeField] public AttackData[] AttackDatas { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public Ragdoll Ragdoll { get; private set; }
    [field: SerializeField] public LedgeDetector LedgeDetector { get; private set; }
    public Transform MainCameraTransform { get; private set; }

    [field: Header("Movement Settings")]
    [field: SerializeField] public float FreeLookMovementSpeed { get; private set; }
    [field: SerializeField] public float TargetingMovementSpeed { get; private set; }
    [field: SerializeField] public float StunTime { get; private set; }
    [field: SerializeField] public float JumpForce { get; private set; }
    
    [field: Header("Dodge Settings")]
    [field: SerializeField] public float DodgeDuration { get; private set; }
    [field: SerializeField] public float DodgeDistance { get; private set; }
    public float PreviousDodgeTime { get; private set; } = Mathf.NegativeInfinity;

    [field: Header("Animator Settings")]
    [field: SerializeField] public float CrossFadeDampTime { get; private set; }
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
        Ragdoll = GetComponent<Ragdoll>();
        LedgeDetector = GetComponentInChildren<LedgeDetector>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SwitchState(new PlayerFreeLookState(this));
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
        SwitchState(new PlayerImpactState(this, stunTime));
    }

    private void HandleDie()
    {
        SwitchState(new PlayerDeadState(this));
    }
}
