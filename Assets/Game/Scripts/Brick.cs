using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public event EventHandler<Brick> onBrickGetDestroyed;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Ball>(out Ball ball))
        {
            onBrickGetDestroyed?.Invoke(this, this);

            gameObject.SetActive(false);
        }
    }
}
