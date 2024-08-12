using UnityEngine;

public class PlayControl : MonoBehaviour
{
    private Rigidbody2D playerRig;

    [HeaderAttribute("人物移动速度")]
    [SerializeField]private float moveSpeed;
    [HeaderAttribute("人物跳跃力度")]
    [SerializeField] private float jumpFroce;
   
    void Start()
    {
        playerRig = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        playerRig.velocity = new Vector2(moveInput * moveSpeed, playerRig.velocity.y);

        //检测物体是否在地面上，防止多次跳跃

        if (Input.GetButtonDown("Jump"))
        {
            playerRig.velocity = new Vector2(playerRig.velocity.x, jumpFroce);
        }
    }
}
