using System;
using UnityEngine;

public class PlayControl : MonoBehaviour
{
    private Rigidbody2D playerRig;
    private Animator anim;
    [Header("人物移动速度")] [SerializeField] private float moveSpeed;
    [Header("人物跳跃力度")] [SerializeField] private float jumpForce;

    [Header("冲刺速度")] [SerializeField] private float dashSpeed;
    [Header("冲刺cd")] [SerializeField] private float dashCd;

    [SerializeField]private float dashCdTimer;
 

    [Header("冲刺持续时间")] [SerializeField] private float dashDuration;

    //冲刺计时器
    [SerializeField] private float dashTime;
    


    [SerializeField]private bool isGrounded = true;
    [SerializeField] private float checkGroundDistance;
    [SerializeField] private LayerMask groundLayer;

    //状态机变量
    private bool isMoving;


    private float moveInput;

    private bool facingRight = true;

    void Start()
    {
        InitView();
    }

    void Update()
    {
        CheckInput();
        Movement();
        FlipController();
        AnimatorControllers();
    }


    /// <summary>
    /// 更新场景中的节点和组件
    /// </summary>
    private void InitView()
    {
        playerRig = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    /// <summary>
    /// 移动
    /// </summary>
    private void Movement()
    {
        if (dashTime > 0)
        {
            playerRig.velocity = new Vector2(moveInput * dashSpeed, 0);
        }
        else
        {
            playerRig.velocity = new Vector2(moveInput * moveSpeed, playerRig.velocity.y);
        }
       
    }

    /// <summary>
    /// 玩家输入检测
    /// </summary>
    /// <returns></returns>
    private void CheckInput()
    {
        //移动
        moveInput = Input.GetAxis("Horizontal");

        //检测是否是地面，防止连跳
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, checkGroundDistance, groundLayer);
        //跳跃
        if (Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }

        if (dashTime > 0)
        {
            //冲刺计时器
            dashTime -= Time.deltaTime;
        }

        if (dashCdTimer > 0)
        {
            //cd
            dashCdTimer -= Time.deltaTime;
        }
     
        //冲刺
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Dash();
        }
    }

    /// <summary>
    /// 实现冲刺的方法
    /// </summary>
    private void Dash()
    {
        if (dashCdTimer <= 0)
        {
            dashTime = dashDuration;
            dashCdTimer = dashCd;
        }
   
    }

    /// <summary>
    /// 跳跃功能实现
    /// </summary>
    private void Jump()
    {
       
        if (isGrounded)
        {
            playerRig.velocity = new Vector2(playerRig.velocity.x, jumpForce);
        }
    }

    /// <summary>
    /// 角色状态机控制器 
    /// </summary>
    private void AnimatorControllers()
    {
        isMoving = playerRig.velocity.x != 0;
        anim.SetFloat("yVelocity", playerRig.velocity.y);
        anim.SetBool("isMove", isMoving);
        anim.SetBool("isGround", isGrounded);
        anim.SetBool("isDash", dashTime > 0);
    }

    /// <summary>
    /// 翻转
    /// </summary>
    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    /// <summary>
    /// 角色翻转控制器
    /// </summary>
    private void FlipController()
    {
        if (playerRig.velocity.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (playerRig.velocity.x < 0 && facingRight)
        {
            Flip();
        }
    }

    /// <summary>
    /// 辅助画线
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position,
            new Vector3(transform.position.x, transform.position.y - checkGroundDistance));
    }
}