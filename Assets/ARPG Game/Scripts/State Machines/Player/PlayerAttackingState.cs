using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackingState : PlayerBaseState
{
    private float previousFrameTime;
    private bool alreadyAppliedForce;

    private AttackData attackData;

    public PlayerAttackingState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine)
    {
        attackData = stateMachine.AttackDatas[attackIndex];
    }

    public override void Enter()
    {
        stateMachine.WeaponHandler.SetAttackDamage(attackData.BaseDamage, attackData.Knockback);
        stateMachine.WeaponHandler.SetStunTime(stateMachine.StunTime);
        stateMachine.Animator.CrossFadeInFixedTime(attackData.AnimationName, attackData.TransitionDuration);
    }

    public override void Tick(float deltaTime)
    {
        if(stateMachine.InputReader.IsBlocking)
        {
            stateMachine.SwitchState(new PlayerBlockingState(stateMachine));
            return;
        }
        
        Move(deltaTime);
        FaceTarget();

        float normalizedTime = GetNormalizedTime(stateMachine.Animator, "Attack");

        if(normalizedTime >= attackData.ForceTime)
        {
            TryApllyForce();
        }

        if(normalizedTime >= previousFrameTime && normalizedTime < 1f)
        {
            if(stateMachine.InputReader.IsAttacking)
            {
                TryComboAttack(normalizedTime);
            }
        } 
        else
        {
            if(stateMachine.Targeter.CurrentTarget != null)
            {
                stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
            }
            else
            {
                stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            }
        }

        previousFrameTime = normalizedTime;
    }

    public override void Exit()
    {
        
    }

    private void TryComboAttack(float normalizedTime)
    {
        if(attackData.ComboStateIndex == -1) { return; }

        if(normalizedTime < attackData.ComboAttackTime) { return; }

        stateMachine.SwitchState(new PlayerAttackingState(stateMachine, attackData.ComboStateIndex));
    }

    private void TryApllyForce()
    {
        if(alreadyAppliedForce) { return; }
        alreadyAppliedForce = true;
        stateMachine.ForceReciever.AddForce(stateMachine.transform.forward * attackData.ForceAmount);
    }
}
