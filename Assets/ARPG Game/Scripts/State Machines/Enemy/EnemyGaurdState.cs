using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class EnemyGaurdState : EnemyBaseState
{
    private readonly int LocomotionBlendTreeHash = Animator.StringToHash("Locomotion");
    private readonly int SpeedHash = Animator.StringToHash("Speed");

    private float waitTime = 0f;
    private float timer = 0f;

    public EnemyGaurdState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(LocomotionBlendTreeHash, stateMachine.CrossFadeDampTime);
        timer = 0f;
        waitTime = stateMachine.SuspicionTime;
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);

        timer += deltaTime;

        // Check if the player is in range to chase
        if (IsPlayerInRange(stateMachine.PlayerChasingRange))
        {
            stateMachine.SwitchState(new EnemyChaseState(stateMachine));
            return;
        }

        if(timer < waitTime)
        {
            stateMachine.Animator.SetFloat(SpeedHash, 0f, stateMachine.AnimatorDampTime, deltaTime);
            return;
        }

        if (timer >= waitTime)
        {
            ReturnToPost(deltaTime);
            ReturnToGaurdingLocation(deltaTime);
        }
        
    }

    public override void Exit()
    {
        
    }

    private void ReturnToPost(float deltaTime)
    {
        if (Vector3.Distance(stateMachine.NavAgent.transform.position, stateMachine._guardPosition) <= stateMachine.NavAgent.stoppingDistance)
        {
            stateMachine.Animator.SetFloat(SpeedHash, 0f, stateMachine.AnimatorDampTime, deltaTime);

            if (stateMachine.transform.rotation != Quaternion.Euler(stateMachine._guardRotation))
            {
                ReturnToGaurdPosition();
            }
        }
        else
        {
            FacePoint(stateMachine._guardPosition);
            stateMachine.Animator.SetFloat(SpeedHash, 1f, stateMachine.AnimatorDampTime, deltaTime);
        }
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
