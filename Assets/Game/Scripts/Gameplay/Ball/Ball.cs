using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField, ChildGameObjectsOnly] private Rigidbody2D ballRb;
    [SerializeField] private float speed;

    public Rigidbody2D BallRb { get => ballRb; set => ballRb = value; }

    private void Start()
    {
        Invoke(nameof(LaunchInRandomTragectory), 1f);
    }

    private void LaunchInRandomTragectory()
    {
        Vector2 force = Vector2.zero;
        force.x = Random.Range(-1f, 1f);
        force.y = -1f;
        ballRb.AddForce(force.normalized * speed);
    }
}
