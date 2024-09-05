using System;
using UnityEngine;

public class PlayControl : MonoBehaviour
{
  
    private Rigidbody2D playerRig;
    private Animator anim;
    [Header("人物移动速度")]
    [SerializeField]private float moveSpeed;
    [Header("人物跳跃力度")]
    [SerializeField] private float jumpFroce;

    private bool isGrounded;
    [SerializeField] private float checkGroundDistance;
    [SerializeField] private LayerMask groundLayer;
    
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
        playerRig.velocity = new Vector2(moveInput * moveSpeed, playerRig.velocity.y);
    }

    /// <summary>
    /// 玩家输入检测
    /// </summary>
    /// <returns></returns>
    private float CheckInput()
    {
        moveInput = Input.GetAxis("Horizontal");
        //检测是否是地面，防止连跳
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, checkGroundDistance, groundLayer);
        if (Input.GetButtonDown("Jump")&&isGrounded)
        {
            Jump();
        }

        return moveInput;
    }

    /// <summary>
    /// 跳跃
    /// </summary>
    private void Jump()
    {
        playerRig.velocity = new Vector2(playerRig.velocity.x, jumpFroce);
    }

    /// <summary>
    /// 角色状态机控制器 
    /// </summary>
    private void AnimatorControllers()
    {
        isMoving = playerRig.velocity.x != 0;
        anim.SetBool("isMove", isMoving);
    }

    /// <summary>
    /// 翻转
    /// </summary>
    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
///////////
    /// <summary>
    /// 角色翻转控制器
    /// </summary>
    private void FlipController()
    {
        if (playerRig.velocity.x>0 && !facingRight)
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
        Gizmos.DrawLine(transform.position,new Vector3(transform.position.x,transform.position.y-checkGroundDistance));
    }
}
