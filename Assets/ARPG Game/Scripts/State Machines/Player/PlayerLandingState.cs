using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandingState : PlayerBaseState
{
    private readonly int LandingHash = Animator.StringToHash("Land");
    private Vector3 momentum;

    public PlayerLandingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        momentum = stateMachine.PlayerController.velocity;
        momentum.y = 0f;

        stateMachine.Animator.CrossFadeInFixedTime(LandingHash, stateMachine.CrossFadeDampTime);
        
        stateMachine.LedgeDetector.OnLedgeDetected += HandleLedgeDetected;
    }

    public override void Tick(float deltaTime)
    {
        Move(momentum, deltaTime);

        if (stateMachine.PlayerController.isGrounded)
        {
            ReturnToLocomotion();
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
