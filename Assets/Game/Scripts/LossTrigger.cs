using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LossTrigger : MonoBehaviour
{
    public event EventHandler onBallReachTheLossTrigger;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            onBallReachTheLossTrigger?.Invoke(this, null);
        }
    }
}
