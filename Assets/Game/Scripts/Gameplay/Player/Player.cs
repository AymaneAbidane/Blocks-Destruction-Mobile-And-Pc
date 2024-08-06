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
}
