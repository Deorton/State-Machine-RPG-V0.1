using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFreeLookState : PlayerBaseState
{
    public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        
    }

    public override void Enter()
    {
       stateMachine.InputReader.JumpEvent += OnJump;
       stateMachine.InputReader.DodgeEvent += OnDodge;
    }

    public override void Tick(float deltaTime)
    {
        // Called every frame while in this state
        Debug.Log("In Free Look State");
    }

    public override void Exit()
    {
        stateMachine.InputReader.JumpEvent -= OnJump;
        stateMachine.InputReader.DodgeEvent -= OnDodge;
    } 

    private void OnJump()
    {
        Debug.Log("Jumping!");
    }

    private void OnDodge()
    {
        Debug.Log("Dodging!");
    }
}

