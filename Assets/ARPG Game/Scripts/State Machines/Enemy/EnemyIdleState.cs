using System.Collections;
using System.Collections.Generic;
using RPGCharacterAnims.Actions;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    private readonly int LocomotionBlendTreeHash = Animator.StringToHash("Locomotion");
    private readonly int SpeedHash = Animator.StringToHash("Speed");

    public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(LocomotionBlendTreeHash, stateMachine.CrossFadeDampTime);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        // Check if the player is in range to chase
        if(IsPlayerInRange(stateMachine.PlayerChasingRange))
        {
            stateMachine.SwitchState(new EnemyChaseState(stateMachine));
            return;
        }
       
        stateMachine.Animator.SetFloat(SpeedHash, 0f, stateMachine.AnimatorDampTime, deltaTime);
    }

    public override void Exit()
    {
        
    }
}
