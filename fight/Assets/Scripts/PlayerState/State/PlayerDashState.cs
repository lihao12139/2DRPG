using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    private float _dashDuration;
 
    public override void Enter()
    {
        base.Enter();
        _dashDuration= dashDuration;
       
    }

    public override void Update()
    {
        base.Update();
       
        
        dashDuration -= Time.deltaTime;
        player.SetVelocity(player.dashDirection * dashSpeed, rb.velocity.y);
        if (dashDuration<0)
        {
            stateMachine.ChangeState(player.idleState);
            dashDuration = _dashDuration;
        }
      
    }

    public override void Exit()
    {
       
        base.Exit();
    }
}
