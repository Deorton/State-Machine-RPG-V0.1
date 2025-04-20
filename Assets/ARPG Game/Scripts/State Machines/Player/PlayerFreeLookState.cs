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
        Vector3 movement = new Vector3();
        
        // Get the movement input from the InputReader and set the y component to 0
        // to prevent vertical movement when the player is on the ground.
        movement.x = stateMachine.InputReader.MovementValue.x;
        movement.y = 0f;
        movement.z = stateMachine.InputReader.MovementValue.y;

        stateMachine.PlayerController.Move(movement * deltaTime);
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

