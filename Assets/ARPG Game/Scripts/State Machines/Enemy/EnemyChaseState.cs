using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    private readonly int LocomotionBlendTreeHash = Animator.StringToHash("Locomotion");
    private readonly int SpeedHash = Animator.StringToHash("Speed");

    public EnemyChaseState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("Entered Chase State");
        stateMachine.Animator.CrossFadeInFixedTime(LocomotionBlendTreeHash, stateMachine.CrossFadeDampTime);
    }

    public override void Tick(float deltaTime)
    {
        if(IsPlayerInRange(stateMachine.PlayerAttackingRange))
        {
            stateMachine.SwitchState(new EnemyAttackingState(stateMachine));
            return;
        }

        if(!IsPlayerInRange(stateMachine.PlayerChasingRange))
        {
            if(stateMachine.IsGaurding)
            {
                stateMachine.SwitchState(new EnemyGaurdState(stateMachine));
            }
            else
            {
                stateMachine.SwitchState(new EnemyIdleState(stateMachine));
            }
            
            return;
        }

        MoveTowardsPlayer(deltaTime);

        FacePlayer();

        stateMachine.Animator.SetFloat(SpeedHash, 1f, stateMachine.AnimatorDampTime, deltaTime);
    }

    public override void Exit()
    {
        stateMachine.NavAgent.ResetPath();
        stateMachine.NavAgent.velocity = Vector3.zero;
    }

    private void MoveTowardsPlayer(float deltaTime)
    {
        if(stateMachine.NavAgent.isOnNavMesh)
        {
            stateMachine.NavAgent.SetDestination(stateMachine.Player.transform.position);
            Move(stateMachine.NavAgent.desiredVelocity.normalized * stateMachine.ChaseMovementSpeed, deltaTime);
        }
        
        stateMachine.NavAgent.velocity = stateMachine.CharController.velocity;
    }
}
