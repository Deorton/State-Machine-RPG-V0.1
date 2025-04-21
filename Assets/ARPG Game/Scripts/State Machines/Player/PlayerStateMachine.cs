using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field: SerializeField] public CharacterController PlayerController { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public InputReader InputReader { get; private set; }
    [field: SerializeField] public float FreeLookMovementSpeed { get; private set; }

    public Transform MainCameraTransform { get; private set; }
    public float animatorDampTime = 0.1f;
    public float rotationDampTime = 0.1f;

    void Awake()
    {
        PlayerController = GetComponent<CharacterController>();
        Animator = GetComponent<Animator>();
        InputReader = GetComponent<InputReader>();
        MainCameraTransform = Camera.main.transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        SwitchState(new PlayerFreeLookState(this));
    }
}
