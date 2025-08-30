using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance { get; private set; }

    [Header("Movement Settings")]
    [SerializeField] private float maxSpeed;        //최대 속도
    [SerializeField] private float acceleration;    //가속도
    [SerializeField] private float deceleration;    //감속도



    private PlayerInputAction playerInputAction;
    private PlayerClimb playerClimb;
    private PlayerZipline playerZipline;
    private PlayerVital playerVital;
    private new Rigidbody2D rigidbody2D;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        playerInputAction = new PlayerInputAction();
        playerInputAction.Player.Enable();

        playerClimb = GetComponent<PlayerClimb>();
        playerZipline = GetComponent<PlayerZipline>();
        playerVital = GetComponent<PlayerVital>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {   if (CanMove())
        { 
            Move();
        }
    }

    private void OnDestroy()
    {
        playerInputAction.Player.Disable();
    }

    private void Move()
    {
        float input = playerInputAction.Player.Movement.ReadValue<float>();
        Vector2 inputVector = new Vector2(input, 0);
        Vector2 xVelocity = new Vector2(rigidbody2D.linearVelocityX, 0);
        Vector2 deltaVelocity = inputVector.normalized * maxSpeed - xVelocity;
        float accelRate = (inputVector.magnitude > 0.1f) ? acceleration : deceleration;

        Vector2 force = deltaVelocity * acceleration;

        rigidbody2D.AddForce(force, ForceMode2D.Force);
    }

    private bool CanMove()
    {
        return !playerClimb.isClimbing &&
        !playerZipline.isZiplining &&
        !playerVital.isDead;
    }
}