using Sirenix.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;

    PlayerControllers controls;

    private float direction;

    private Vector3 moveVector;

    private const float MAX_BOUNCE_ANGLE = 75f;
    private const float CLAMP_VALUE = 7.5f;

    private void Awake()
    {
        GetsInputsValuesFromInputSyteme();
    }

    private void GetsInputsValuesFromInputSyteme()
    {
        controls = new();
        controls.Enable();
        controls.Controlle.Move.performed += controllX =>
        {
            direction = controllX.ReadValue<float>();
        };
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDestroy()
    {
        controls.Disable();
    }

    private void Update()
    {
        GetInputs();
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void GetInputs()
    {
        moveVector = Vector3.right * direction * speed;
    }

    private void Move()
    {
        transform.position += moveVector * Time.fixedDeltaTime;
        ClampPosition();
    }

    private void ClampPosition()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -CLAMP_VALUE, CLAMP_VALUE), transform.position.y);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Ball>(out Ball ball))
        {
            Vector3 playerPos = transform.position;
            Vector2 contactPoint = collision.GetContact(0).point;

            float offset = playerPos.x - contactPoint.x;
            float width = collision.otherCollider.bounds.size.x / 2;

            float currentAngle = Vector2.SignedAngle(Vector2.up, ball.BallRb.velocity);
            float bounceAngle = (offset / width) * MAX_BOUNCE_ANGLE;
            float newAngle = Mathf.Clamp(currentAngle + bounceAngle, -MAX_BOUNCE_ANGLE, MAX_BOUNCE_ANGLE);

            Quaternion rotation = Quaternion.AngleAxis(newAngle, Vector3.forward);
            ball.BallRb.velocity = rotation * Vector2.up * ball.BallRb.velocity.magnitude;
        }
    }
}
