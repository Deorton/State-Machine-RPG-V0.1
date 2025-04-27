using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyImpactState : EnemyBaseState
{
    private readonly int ImpactHash = Animator.StringToHash("Knockback");

    private float impactDuration = 1f; // Duration of the impact animation

    public EnemyImpactState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(ImpactHash, stateMachine.CrossFadeDampTime);;
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime); // Call the Move method to apply movement during impact
        
        impactDuration -= deltaTime;

        if (impactDuration <= 0f)
        {
            stateMachine.SwitchState(new EnemyIdleState(stateMachine)); // Switch back to idle state after impact
        }
    }

    public override void Exit()
    {
        
    }
}
