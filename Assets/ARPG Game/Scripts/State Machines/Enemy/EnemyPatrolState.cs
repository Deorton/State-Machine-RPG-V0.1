using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyPatrolState : EnemyBaseState
{

    private readonly int LocomotionBlendTreeHash = Animator.StringToHash("Locomotion");
    private readonly int SpeedHash = Animator.StringToHash("Speed");

    private int _currentWaypointIndex = 0;
    private float waitTime = 0f;
    private float timer = 0f;

    public EnemyPatrolState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.NavAgent.updatePosition = true;
        stateMachine.NavAgent.updateRotation = true;

        stateMachine.Animator.CrossFadeInFixedTime(LocomotionBlendTreeHash, stateMachine.CrossFadeDampTime);
        waitTime = stateMachine.WaypointDwellTime;
        _currentWaypointIndex = stateMachine._currentWaypointIndex;
    }

    public override void Tick(float deltaTime)
    {
        timer += deltaTime;

        if (IsPlayerInRange(stateMachine.PlayerChasingRange))
        {
            stateMachine.SwitchState(new EnemyChaseState(stateMachine));
            return;
        }

        Vector3 nextPosition = stateMachine._guardPosition;
        
        if(timer < waitTime)
        {
            if (stateMachine.NavAgent.velocity.magnitude <= 0.6f)
            {
                stateMachine.Animator.SetFloat(SpeedHash, 0f, stateMachine.AnimatorDampTime, deltaTime);
            }
            return;
        }

        if (stateMachine.PatrolPath != null)
        {
            if (AtWaypoint())
            {
                timer = 0f;
                CycleWaypoint();
            }
            
            nextPosition = GetCurrentWaypoint();
            stateMachine._currentWaypointIndex = _currentWaypointIndex;

            if (timer >= waitTime)
            {
                stateMachine.Animator.SetFloat(SpeedHash, 0.5f, stateMachine.AnimatorDampTime, deltaTime);

                MoveToNextWaypoint(deltaTime, nextPosition);
                faceNextWaypoint(nextPosition);
            }
        }

         
    }

    public override void Exit()
    {
        stateMachine.NavAgent.updatePosition = false;
        stateMachine.NavAgent.updateRotation = false;
    }

    private bool AtWaypoint()
    {
        if (Vector3.Distance(stateMachine.NavAgent.transform.position, GetCurrentWaypoint()) <= stateMachine.NavAgent.stoppingDistance)
        {
            return true;

        }
        else
        {
            return false;
        }
    }

    private void CycleWaypoint()
    {
        _currentWaypointIndex = stateMachine.PatrolPath.GetNextIndex(_currentWaypointIndex);
    }

    private Vector3 GetCurrentWaypoint()
    {
        return stateMachine.PatrolPath.GetWaypoint(_currentWaypointIndex);
    }

    private void MoveToNextWaypoint(float deltaTime, Vector3 WaypointPosition)
    {
        if (stateMachine.NavAgent.isOnNavMesh)
        {
            stateMachine.NavAgent.SetDestination(WaypointPosition);
            Move(stateMachine.NavAgent.desiredVelocity.normalized * stateMachine.PatrolMovementSpeed, deltaTime);
        }

        stateMachine.NavAgent.velocity = stateMachine.CharController.velocity;
    }
    
    private void faceNextWaypoint(Vector3 nextPosition)
    {
        Vector3 directionToTarget = nextPosition - stateMachine.transform.position;
        directionToTarget.y = 0f;

        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
        stateMachine.transform.rotation = Quaternion.Slerp(stateMachine.transform.rotation, targetRotation, stateMachine.rotationDampTime);
    }
}
