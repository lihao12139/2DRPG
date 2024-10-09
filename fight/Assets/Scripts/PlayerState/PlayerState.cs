using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerState 
{
    protected Player player;
    protected PlayerStateMachine stateMachine;
    private string animBoolName;

    protected Rigidbody2D rb;
    //获取玩家的输入信息，子物体可能用到
    protected float xInput;

    //玩家属性从这里获取
    
    //速度
    public float moveSpeed = PlayerInfo.moveSpeed;
    //跳跃高度
    public float jumpSpeed = PlayerInfo.jumpSpeed;
    //冲刺速度
    public float dashSpeed = PlayerInfo.dashSpeed;
    //冲刺持续时间
    public float dashDuration = PlayerInfo.dashDuration;
    //冲刺cd
    public float dashCd = PlayerInfo.dashCd;
    //空中移动速度
    public float airSpeed = PlayerInfo.airSpeed;
    
    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
 
    }

    public virtual void Enter()
    {
        rb = player.rb;
        player.anim.SetBool(animBoolName, true);
        
    }

    public virtual void Update()
    {
        //获取玩家移动信息
        xInput = Input.GetAxisRaw("Horizontal");
        //获取玩家在空中的速度
        player.anim.SetFloat("yVelocity", rb.velocity.y);
    }

    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);
    }
}
