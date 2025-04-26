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
        stateMachine.WeaponHandler.SetAttackDamage(attackData.BaseDamage);
        stateMachine.Animator.CrossFadeInFixedTime(attackData.AnimationName, attackData.TransitionDuration);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        FaceTarget();

        float normalizedTime = GetNormalizedTime();

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

    private float GetNormalizedTime()
    {
        AnimatorStateInfo currentStateInfo = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextStateInfo = stateMachine.Animator.GetNextAnimatorStateInfo(0);

        if(stateMachine.Animator.IsInTransition(0) && nextStateInfo.IsTag("Attack"))
        {
            return nextStateInfo.normalizedTime;
        }
        else if(!stateMachine.Animator.IsInTransition(0) && currentStateInfo.IsTag("Attack"))
        {
            return currentStateInfo.normalizedTime;
        }
        else
        {
            return 0f;
        }
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
