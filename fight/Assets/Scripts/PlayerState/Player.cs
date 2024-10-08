using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Components
    public Animator anim { get; private set; }
    
    public Rigidbody2D rb { get; private set; }
    
    #endregion
    
    #region States
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    
    public PlayerJumpState jumpState { get; private set; }
    
    public PlayerAirStates airState { get; private set; }
    
    public PlayerDashState DashState { get; private set; }
    
    #endregion

    [Header("碰撞信息")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask whatIsGround;

     public float dashDirection { get; private set; }
     public float dashCd = PlayerInfo.dashCd;
    public int facingDirection { get; private set; } = 1;
    private bool facingRight = true;
    
    private void Awake()
    {
        //初始化状态
        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirStates(this, stateMachine, "Jump");
        DashState = new PlayerDashState(this, stateMachine, "Dash");
    }

    private void Start()
    {
        //初始化组件
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        //初始状态
        stateMachine.Initialize(idleState);
    }

    private void Update()
    {
        //执行当前状态的update周期
        stateMachine.currentState.Update();

        //检测是否在冲刺状态
        CheckDashInput();
    }

    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.velocity = new Vector3(_xVelocity, _yVelocity);
        FilpController(_xVelocity);
    }

    /// <summary>
    /// 检测是不是地面
    /// </summary>
    /// <returns></returns>
    public bool IsGrounded()
    {
        return Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    }
    
    
    /// <summary>
    /// 画一条辅助线来检测距离
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckDistance);
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + Vector3.right * wallCheckDistance);
    }

    private void Filp()
    {
        facingDirection = facingDirection * -1;
        facingRight = !facingRight;
        transform.Rotate(0,180,0);
    }

    public void FilpController(float _xVelocity)
    {s
        if (_xVelocity > 0 && !facingRight)
        {
            Filp();
        }
        else if(_xVelocity < 0 && facingRight)
        {
            Filp();
        }
    }

    private void CheckDashInput()
    {
        if (dashCd > 0)
        {
            dashCd -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift)&&dashCd<0)
        {
            dashDirection = Input.GetAxisRaw("Horizontal");
            if (dashDirection == 0)
                dashDirection = facingDirection;
            stateMachine.ChangeState(DashState);
            dashCd = PlayerInfo.dashCd;
        }
    }
}