using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float maxSpeed;        //최대 속도
    [SerializeField] private float acceleration;    //가속도
    [SerializeField] private float deceleration;    //감속도

    

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
        Move();
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
}