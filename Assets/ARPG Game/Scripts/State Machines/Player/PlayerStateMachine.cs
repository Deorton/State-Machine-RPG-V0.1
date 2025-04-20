using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field: SerializeField] public CharacterController PlayerController { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public InputReader InputReader { get; private set; }

    void Awake()
    {
        PlayerController = GetComponent<CharacterController>();
        Animator = GetComponent<Animator>();
        InputReader = GetComponent<InputReader>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SwitchState(new PlayerFreeLookState(this));
    }
}
