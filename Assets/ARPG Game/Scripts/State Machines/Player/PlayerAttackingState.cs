using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackingState : PlayerBaseState
{
    private AttackData attackData;

    public PlayerAttackingState(PlayerStateMachine stateMachine, int attackId) : base(stateMachine)
    {
        attackData = stateMachine.AttackDatas[attackId];
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(attackData.AnimationName, stateMachine.CrossFadeDampTime);
    }

    public override void Tick(float deltaTime)
    {
        
    }

    public override void Exit()
    {
        
    }
}
