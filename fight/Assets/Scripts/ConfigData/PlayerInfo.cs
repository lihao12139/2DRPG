using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 静态类用于存储玩家的状态信息
/// </summary>
public static class PlayerInfo
{
    //移动速度
    private static float _moveSpeed = 12f;
    //跳跃高度
    private static readonly float _jumpSpeed = 14f;
    //冲刺Cd
    private static readonly float _dashCd = 2f;
    //冲刺持续时间
    private static readonly float _dashDuration = 0.4f;
    //冲刺速度x
    private static readonly float _dashSpeed = 24f;
    
    
    public static  float moveSpeed
    {
        get { return _moveSpeed; }
    }
    public static float jumpSpeed
    {
        get {return _jumpSpeed;}
    }
    public static float dashCd
    {
        get { return _dashCd; }
    }
    public static float dashDuration
    {
        get { return _dashDuration; }
    }
    public static float dashSpeed
    {
        get { return _dashSpeed; }
    }
}
