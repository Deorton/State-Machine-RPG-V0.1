using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHangingState : PlayerBaseState
{
    private readonly int HangingHash = Animator.StringToHash("Hanging ");

    private Vector3 closestPoint;
    private Vector3 ledgeForward;

    public PlayerHangingState(PlayerStateMachine stateMachine, Vector3 closestPoint, Vector3 ledgeForward) : base(stateMachine)
    {
        this.closestPoint = closestPoint;
        this.ledgeForward = ledgeForward;
    }

    public override void Enter()
    {
        stateMachine.transform.rotation = Quaternion.LookRotation(ledgeForward);
        
        stateMachine.PlayerController.enabled = false;
        stateMachine.transform.position = closestPoint - (stateMachine.LedgeDetector.transform.position - stateMachine.transform.position);
        stateMachine.PlayerController.enabled = true;

        stateMachine.Animator.CrossFadeInFixedTime(HangingHash, stateMachine.CrossFadeDampTime);
    }

    public override void Tick(float deltaTime)
    {
        if(stateMachine.InputReader.MovementValue.y < 0)
        {
            stateMachine.PlayerController.Move(Vector3.zero);
            stateMachine.ForceReciever.Reset();
            stateMachine.SwitchState(new PlayerLandingState(stateMachine));
            return;
        }
        
        if(stateMachine.InputReader.MovementValue.y > 0)
        {
            stateMachine.SwitchState(new PlayerPullUpState(stateMachine));
            return;
        }
    }

    public override void Exit()
    {
        stateMachine.ForceReciever.Reset();
    }

    
}
