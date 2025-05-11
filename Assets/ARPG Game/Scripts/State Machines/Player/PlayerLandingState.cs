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
    }
}
