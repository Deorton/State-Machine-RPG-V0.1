using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpingState : PlayerBaseState
{
    private readonly int JumpingHash = Animator.StringToHash("Jump");
    private Vector3 momentum;

    public PlayerJumpingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.ForceReciever.Jump(stateMachine.JumpForce);
        momentum = stateMachine.PlayerController.velocity;
        momentum.y = 0f;

        stateMachine.Animator.CrossFadeInFixedTime(JumpingHash, stateMachine.CrossFadeDampTime);

        stateMachine.LedgeDetector.OnLedgeDetected += HandleLedgeDetected;
    }

    public override void Tick(float deltaTime)
    {
        Move(momentum, deltaTime);

        if(stateMachine.PlayerController.velocity.y <= 0f)
        {
            stateMachine.SwitchState(new PlayerLandingState(stateMachine));
            return;
        }

        FaceTarget();
    }

    public override void Exit()
    {
        stateMachine.LedgeDetector.OnLedgeDetected -= HandleLedgeDetected;
    }

    private void HandleLedgeDetected(Vector3 closestPoint, Vector3 ledgeForward)
    {
        stateMachine.SwitchState(new PlayerHangingState(stateMachine, closestPoint, ledgeForward));
    }
}
