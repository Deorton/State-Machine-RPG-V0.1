using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGaurdState : EnemyBaseState
{
    private readonly int LocomotionBlendTreeHash = Animator.StringToHash("Locomotion");
    private readonly int SpeedHash = Animator.StringToHash("Speed");

    

    public EnemyGaurdState(EnemyStateMachine stateMachine) : base(stateMachine)
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
        
        if (Vector3.Distance (stateMachine.NavAgent.transform.position, stateMachine._guardPosition) <= stateMachine.NavAgent.stoppingDistance) 
        {
            stateMachine.Animator.SetFloat(SpeedHash, 0f, stateMachine.AnimatorDampTime, deltaTime);
            
            if(stateMachine.transform.rotation != Quaternion.Euler(stateMachine._guardRotation))
            {
                ReturnToGaurdPosition();
            }
        }
        else
        {
            FacePoint(stateMachine._guardPosition);
            stateMachine.Animator.SetFloat(SpeedHash, 1f, stateMachine.AnimatorDampTime, deltaTime);
        }

        ReturnToGaurdingLocation(deltaTime);
    }

    public override void Exit()
    {
        
    }

    private void ReturnToGaurdingLocation(float deltaTime)
    {
        if(stateMachine.NavAgent.isOnNavMesh)
        {
            stateMachine.NavAgent.SetDestination(stateMachine._guardPosition);
            Move(stateMachine.NavAgent.desiredVelocity.normalized * stateMachine.PatrolMovementSpeed, deltaTime);
        }
        
        stateMachine.NavAgent.velocity = stateMachine.CharController.velocity;
    }

    private void ReturnToGaurdPosition()
    {
        stateMachine.transform.rotation = Quaternion.Slerp(stateMachine.transform.rotation, Quaternion.Euler(stateMachine._guardRotation), stateMachine.rotationDampTime);
    }
}
