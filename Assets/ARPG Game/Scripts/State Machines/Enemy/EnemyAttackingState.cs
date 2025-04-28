using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackingState : EnemyBaseState
{
    private readonly int AttackHash = Animator.StringToHash("Attack");

    public EnemyAttackingState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.WeaponHandler.SetAttackDamage(stateMachine.Damage, stateMachine.AttackKnockback);
        stateMachine.WeaponHandler.SetStunTime(stateMachine.StunTime);
        stateMachine.Animator.CrossFadeInFixedTime(AttackHash, stateMachine.CrossFadeDampTime);
    }

    public override void Tick(float deltaTime)
    {
        FacePlayer();
        
        if(GetNormalizedTime(stateMachine.Animator) >= 1)
        {
            stateMachine.SwitchState(new EnemyChaseState(stateMachine));
        }
    }

    public override void Exit()
    {
        
    }

    
}
