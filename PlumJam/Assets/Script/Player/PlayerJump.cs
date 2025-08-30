using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    [Header("Jump Settings")]
    [SerializeField] private float jumpHeight;      //점프 높이
    [SerializeField] private int maxJumpCount;      //최대 점프 횟수
    [SerializeField] private float freefallGravityScale; //자유 낙하 시 중력 배율
    [SerializeField, Range(0, 90f)] private float slopeAngleThreshold; //경사면 각도 임계값 (이 각도 이하일 때 바닥 간주)

    private PlayerInputAction playerInputAction;
    private PlayerClimb playerClimb;
    private PlayerZipline playerZipline;
    private new Rigidbody2D rigidbody2D;
    private int currentJumpCount;                   //현재 점프 횟수
    private float originalGravityScale;             //원래 중력 배율

    private Transform hitGroundTransform;           //바닥에 닿은 물체의 Transform

    private void Awake()
    {
        playerInputAction = new PlayerInputAction();
        playerInputAction.Player.Enable();
        playerInputAction.Player.Jump.performed += (InputAction.CallbackContext context) => Jump();

        //사다리 타기 시작 시 점프 횟수 초기화
        PlayerClimb.OnClimbEnter += (s, e) => ResetJumpCount();

        rigidbody2D = GetComponent<Rigidbody2D>();
        playerClimb = GetComponent<PlayerClimb>();
        playerZipline = GetComponent<PlayerZipline>();

        currentJumpCount = maxJumpCount;
        originalGravityScale = rigidbody2D.gravityScale;
    }

    private void Update()
    {
        if (CanSetFreefallGravity())
        { 
            SetFreefallGravity(); 
        }
    }

    private void OnDestroy()
    {
        playerInputAction.Player.Disable();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        CheckGrounded(collision.contacts);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform == hitGroundTransform)
        {
            hitGroundTransform = null;
        }
    }

    //점프 시도
    private void Jump()
    {
        if (currentJumpCount <= 0) return;  //점프 횟수 초과 시 리턴
        if (playerClimb.isClimbing)         //사다리 타고 있을 때 점프 시 사다리 타기 종료
        {
            playerClimb.ExitClimbMode();
        }

        float force = GetJumpForce();

        rigidbody2D.linearVelocity = new Vector2(rigidbody2D.linearVelocityX, 0); //점프 직전 수직 속도 초기화
        rigidbody2D.AddForce(Vector2.up * force, ForceMode2D.Impulse);
        currentJumpCount--;

        Debug.Log($"JumpForce : {force}");
    }

    //점프 횟수 초기화
    public void ResetJumpCount()
    {
        currentJumpCount = maxJumpCount;
    }

    //닿은 물체 중 바닥인지 확인하기
    private void CheckGrounded(ContactPoint2D[] contacts)
    {
        float slopeThreshold = Mathf.Cos(slopeAngleThreshold * Mathf.Deg2Rad);

        foreach (ContactPoint2D contact in contacts)
        {
            if (contact.normal.y > slopeThreshold && contact.collider.transform != hitGroundTransform)
            {
                //바닥에 닿고 그 바닥이 이전에 닿았던 바닥과 같으면 중복 방지
                ResetJumpCount();
                hitGroundTransform = contact.collider.transform;
                break;
            }
            else if (contact.normal.y < -slopeThreshold)
            {
                ResetJumpCount();

                Debug.Log("Grounded!");
                break;
            }
        }
    }

    private float GetJumpForce()
    {
        float gravity = Mathf.Abs(Physics2D.gravity.y * originalGravityScale);
        float jumpVelocity = Mathf.Sqrt(2 * gravity * jumpHeight);
        float force = rigidbody2D.mass * jumpVelocity;

        return force;
    }

    private void SetFreefallGravity()
    {
        rigidbody2D.gravityScale = (rigidbody2D.linearVelocityY < 0) ? freefallGravityScale : originalGravityScale;
    }

    private bool CanSetFreefallGravity()
    {
        return !playerClimb.isClimbing &&
        !playerZipline.isZiplining;
    }
}
