using System.Collections;
using System.Collections.Generic;
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
        Debug.Log("Enter Idle State");
        stateMachine.Animator.CrossFadeInFixedTime(LocomotionBlendTreeHash, stateMachine.CrossFadeDampTime);
        
    }

    public override void Tick(float deltaTime)
    {
        stateMachine.Animator.SetFloat(SpeedHash, 0f, stateMachine.AnimatorDampTime, deltaTime);
    }

    public override void Exit()
    {
        
    }
}
