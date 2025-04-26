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

    protected bool IsPlayerInChasingRange()
    {
        return Vector3.Distance(stateMachine.transform.position, stateMachine.Player.transform.position) <= stateMachine.PlayerChasingRange;
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
