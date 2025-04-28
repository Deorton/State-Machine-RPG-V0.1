using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    private readonly int TargetingBlendTreeHash = Animator.StringToHash("TargetingBlendTree");
    private readonly int TargetingForwardSpeedHash = Animator.StringToHash("TargetingForwardSpeed");
    private readonly int TargetingRightSpeedHash = Animator.StringToHash("TargetingRightSpeed");

    private Vector2 dodgingDirection;
    private float remainingDodgeTime;

    public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(TargetingBlendTreeHash, stateMachine.CrossFadeDampTime);
        stateMachine.InputReader.CancelEvent += OnCanceled;
        stateMachine.InputReader.DodgeEvent += OnDodge;
    }

    public override void Tick(float deltaTime)
    {
        if(stateMachine.InputReader.IsAttacking)
        {
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine, 0));
            return;
        }

        if(stateMachine.InputReader.IsBlocking)
        {
            stateMachine.SwitchState(new PlayerBlockingState(stateMachine));
            return;
        }

        if(stateMachine.Targeter.CurrentTarget == null)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
        }

        Vector3 movement = CalculateMovement();
        Move(movement * stateMachine.TargetingMovementSpeed, deltaTime);

        UpdateAnimator(deltaTime);

        FaceTarget();
    }

    public override void Exit()
    {
        stateMachine.InputReader.CancelEvent -= OnCanceled;
        stateMachine.InputReader.DodgeEvent -= OnDodge;
    }

    private void OnCanceled()
    {
        stateMachine.Targeter.Cancel();
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }

    private void OnDodge()
    {
        if(Time.time - stateMachine.PreviousDodgeTime < stateMachine.DodgeCooldown)
        {
            return;
        }
        
        stateMachine.SetDodgeTime(Time.time);
        dodgingDirection = stateMachine.InputReader.MovementValue;
        remainingDodgeTime = stateMachine.DodgeDuration;
    }

    private Vector3 CalculateMovement()
    { 
        Vector3 movement = new Vector3();
        
        if(remainingDodgeTime > 0f)
        {
            movement += stateMachine.transform.right * dodgingDirection.x * stateMachine.DodgeDistance / stateMachine.DodgeDuration;
            movement += stateMachine.transform.forward * dodgingDirection.y * stateMachine.DodgeDistance / stateMachine.DodgeDuration;
            remainingDodgeTime -= Time.deltaTime;

            if(remainingDodgeTime < 0f)
            {
                remainingDodgeTime = 0f;
            }
        }
        else
        {
            movement += stateMachine.transform.right * stateMachine.InputReader.MovementValue.x;
            movement += stateMachine.transform.forward * stateMachine.InputReader.MovementValue.y;
        }
        

        return movement; 
    }

    private void UpdateAnimator(float deltaTime)
    {
        stateMachine.Animator.SetFloat(TargetingForwardSpeedHash, stateMachine.InputReader.MovementValue.y, stateMachine.animatorDampTime, deltaTime);
        stateMachine.Animator.SetFloat(TargetingRightSpeedHash, stateMachine.InputReader.MovementValue.x, stateMachine.animatorDampTime, deltaTime);
    }
}
