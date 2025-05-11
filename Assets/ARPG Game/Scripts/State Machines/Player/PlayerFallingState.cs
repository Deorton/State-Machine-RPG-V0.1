using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallingState : PlayerBaseState
{
    private readonly int FallingHash = Animator.StringToHash("Falling");

    public PlayerFallingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(FallingHash, stateMachine.CrossFadeDampTime);
    }

    public override void Tick(float deltaTime)
    {
        
    }

    public override void Exit()
    {
        
    }
}
