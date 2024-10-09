using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    // Start is called before the first frame update
    public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }



    public override void Enter()
    {
        base.Enter();
        rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
    }

    public override void Update()
    {
        base.Update();
       
        if (rb.velocity.y < 0)
        {
            stateMachine.ChangeState(player.airState);
        }
        if (xInput != 0)
        {
            player.SetVelocity(airSpeed * xInput,rb.velocity.y);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
