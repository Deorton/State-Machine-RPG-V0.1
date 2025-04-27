using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerImpactState : PlayerBaseState
{
    private readonly int ImpactHash = Animator.StringToHash("Knockback");
    private float impactDuration = 1f; // Duration of the impact animation

    public PlayerImpactState(PlayerStateMachine stateMachine, float stunTime) : base(stateMachine)
    {
        impactDuration = stunTime; // Set the impact duration based on the stun time received
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(ImpactHash, stateMachine.CrossFadeDampTime);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime); // Call the Move method to apply movement during impact

        impactDuration -= deltaTime;

        if (impactDuration <= 0f)
        {
            ReturnToLocomotion(); // Switch back to idle state after impact
        }
    }

    public override void Exit()
    {
        
    }

    
}
