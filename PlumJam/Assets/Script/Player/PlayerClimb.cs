using System;
using UnityEngine;

public class PlayerClimb : MonoBehaviour
{
    [Header("Climb Settings")]
    [SerializeField] private float climbSpeed;      //등반 속도
    public bool isClimbing { get; private set; }    //등반 중인지 여부
    public static event EventHandler OnClimbEnter;

    private PlayerInputAction playerInputAction;
    private new Rigidbody2D rigidbody2D;

    private void Awake()
    {
        playerInputAction = new PlayerInputAction();
        playerInputAction.Player.Enable();

        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (isClimbing)
        { 
            Climb();
        }
    }

    private void OnDestroy()
    {
        playerInputAction.Player.Disable();
        OnClimbEnter = null;
    }

    public void EnterClimbMode()
    {
        if (isClimbing) return;

        isClimbing = true;
        rigidbody2D.linearVelocity = Vector2.zero;
        rigidbody2D.gravityScale = 0f;

        OnClimbEnter?.Invoke(this, EventArgs.Empty);
    }

    public void ExitClimbMode()
    {
        isClimbing = false;
        rigidbody2D.gravityScale = 1f;
    }

    private void Climb()
    {
        float input = playerInputAction.Player.Climb.ReadValue<float>();
        Vector2 climbVelocity = new Vector2(0, input * climbSpeed);
        rigidbody2D.linearVelocity = climbVelocity;

        Debug.Log($"Climbing... {rigidbody2D.linearVelocity}");
    }
}
