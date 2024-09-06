using System;
using UnityEngine;

public class PlayControl : MonoBehaviour
{
    private Rigidbody2D playerRig;

    private Animator anim;

    [Header("目前是在场景里面配置信息，后期有时间了换一个表，读表拉取信息")]
    
    //移动info
    [Header("人物移动速度")] [SerializeField] private float moveSpeed;
    private bool isMoving;
    private float moveInput;
    private bool facingRight = true;
    private int facingDir = 1;

    ///////跳跃info
    [Header("人物跳跃力度")] [SerializeField] private float jumpForce;
    [Header("到地面的距离")] [SerializeField] private float checkGroundDistance;
    [Header("地面的层级")] [SerializeField] private LayerMask groundLayer;

    private bool isGrounded = true;

    //////冲刺info
    [Header("冲刺持续时间")] [SerializeField] private float dashDuration;
    [Header("冲刺速度")] [SerializeField] private float dashSpeed;
    [Header("冲刺cd")] [SerializeField] private float dashCd;

    //冲刺cd计时器
    private float dashCdTimer;

    //冲刺持续计时器
    private float dashTime;

    ////////攻击info
    private bool isAttack = false;

    private int comboCounter;

    //攻击间隔
    [Header("攻击重置间隔")] [SerializeField] private float comboTime;

    //计时器
    private float comboTimeCounter;

    void Start()
    {
        InitView();
    }

    void Update()
    {
        Timer();
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
        if (isAttack) //攻击无法移动
        {
            playerRig.velocity = new Vector2(0, 0);
        }
        else if (dashTime > 0) //冲刺移动
        {
            playerRig.velocity = new Vector2(facingDir * dashSpeed, 0);
        }
        else //正常移动
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

        //冲刺
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Dash();
        }

        //攻击
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartAttack();
        }
    }

    /// <summary>
    /// 攻击的逻辑
    /// </summary>
    private void StartAttack()
    {
        if (!isGrounded) return;
        if (comboTimeCounter < 0)
        {
            comboCounter = 0;
        }

        isAttack = true;
        comboTimeCounter = comboTime;
    }

    /// <summary>
    /// 计时器
    /// </summary>
    private void Timer()
    {
        if (dashTime > 0)
        {
            //冲刺计时器
            dashTime -= Time.deltaTime;
        }

        if (dashCdTimer > 0)
        {
            //冲刺cd计时器
            dashCdTimer -= Time.deltaTime;
        }

        if (comboTimeCounter > 0)
        {
            comboTimeCounter -= Time.deltaTime;
        }
    }

    /// <summary>
    /// 实现冲刺的方法
    /// </summary>
    private void Dash()
    {
        if (dashCdTimer <= 0 && !isAttack)
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
        anim.SetBool("isAttack", isAttack);
        anim.SetInteger("comboCounter", comboCounter);
    }


    /// <summary>
    /// 攻击结束
    /// </summary>
    public void AttackOver()
    {
        isAttack = false;
        comboCounter++;
        if (comboCounter > 2) comboCounter = 0;
    }

    /// <summary>
    /// 翻转
    /// </summary>
    private void Flip()
    {
        facingRight = !facingRight;
        facingDir *= -1;
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