using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LossTrigger : MonoBehaviour
{
    public event EventHandler onBallReachTheLossTrigger;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Ball>(out Ball ball))
        {
            onBallReachTheLossTrigger?.Invoke(this, null);
        }
    }
}
