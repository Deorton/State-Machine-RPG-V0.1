using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPullUpState : PlayerBaseState
{
    private readonly int PullUpHash = Animator.StringToHash("PullUp");

    public PlayerPullUpState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(PullUpHash, stateMachine.CrossFadeDampTime);
    }

    public override void Tick(float deltaTime)
    {
        if (GetNormalizedTime(stateMachine.Animator, "ClimbUp") <1f)
        {
            return;
        }

        stateMachine.PlayerController.enabled = false;
        stateMachine.transform.Translate(stateMachine.Xpos, stateMachine.Ypos, stateMachine.Zpos, Space.Self);
        stateMachine.PlayerController.enabled = true;

        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine, false));
    }

    public override void Exit()
    {
        stateMachine.PlayerController.Move(Vector3.zero);
        stateMachine.ForceReciever.Reset();
    }

    
}
