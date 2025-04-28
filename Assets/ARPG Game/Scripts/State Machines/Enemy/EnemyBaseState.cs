using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBaseState : State
{
    protected EnemyStateMachine stateMachine; 

    public EnemyBaseState(EnemyStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    protected void FacePlayer()
    {
        if (stateMachine.Player == null) { return; }

        Vector3 directionToTarget = stateMachine.Player.transform.position - stateMachine.transform.position;
        directionToTarget.y = 0f;

        Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
        stateMachine.transform.rotation = Quaternion.Slerp(stateMachine.transform.rotation, targetRotation, stateMachine.rotationDampTime);
    }

    protected bool IsPlayerInRange(float range)
    {
        if(stateMachine.Player.isDead) { return false; }
        return Vector3.Distance(stateMachine.transform.position, stateMachine.Player.transform.position) <= range;
    }

    protected void Move(float deltaTime)
    {
        Move(Vector3.zero, deltaTime);
    }

    protected void Move(Vector3 motion, float deltaTime)
    {
        stateMachine.CharController.Move((motion + stateMachine.ForceReciever.movement )* deltaTime);
    }

}
