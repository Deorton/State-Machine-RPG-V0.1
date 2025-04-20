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
        // Called when entering the state
        Debug.Log("Entering Free Look State");
    }

    public override void Tick(float deltaTime)
    {
        // Called every frame while in this state
        Debug.Log("In Free Look State");
    }

    public override void Exit()
    {
        // Called when exiting the state
        Debug.Log("Exiting Free Look State");
    } 
}

