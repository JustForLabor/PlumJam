using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    [Header("Jump Settings")]
    [SerializeField] private float jumpForce;       //점프 힘
    [SerializeField] private int maxJumpCount;      //최대 점프 횟수
    [SerializeField] private float freefallGravityScale = 2f; //자유 낙하 시 중력 배율
    [SerializeField, Range(0, 90f)] private float slopeAngleThreshold; //경사면 각도 임계값 (이 각도 이하일 때 바닥 간주)

    private PlayerInputAction playerInputAction;
    private new Rigidbody2D rigidbody2D;
    private int currentJumpCount;                   //현재 점프 횟수
    private float originalGravityScale;             //원래 중력 배율

    private void Awake()
    {
        playerInputAction = new PlayerInputAction();
        playerInputAction.Player.Enable();
        playerInputAction.Player.Jump.performed += Jump_Performed;

        rigidbody2D = GetComponent<Rigidbody2D>();

        currentJumpCount = maxJumpCount;
        originalGravityScale = rigidbody2D.gravityScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CheckGrounded(collision.contacts);
    }

    private void Jump_Performed(InputAction.CallbackContext context)
    {
        Jump();
    }

    //점프 시도
    private void Jump()
    {
        if (currentJumpCount <= 0) return;  //점프 횟수 초과 시 리턴

        rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        currentJumpCount--;
    }

    //닿은 물체 중 바닥인지 확인하기
    private void CheckGrounded(ContactPoint2D[] contacts)
    {   
        float slopeThreshold = Mathf.Cos(slopeAngleThreshold * Mathf.Deg2Rad);

        foreach (ContactPoint2D contact in contacts)
        {
            if (contact.normal.y > slopeThreshold)
            {
                currentJumpCount = maxJumpCount;

                Debug.Log("Grounded!");
                break;
            }
        }
    }
}
