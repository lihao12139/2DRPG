using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirStates : PlayerState
{
    // Start is called before the first frame update
    public PlayerAirStates(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }


    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        if (player.IsGrounded())
        {
            stateMachine.ChangeState(player.idleState);
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
