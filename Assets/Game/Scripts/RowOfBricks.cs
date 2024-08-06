using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowOfBricks : MonoBehaviour
{
    [SerializeField, ChildGameObjectsOnly] private List<Brick> rowOfBricks = new();
    public event EventHandler<RowOfBricks> onBricksOnTheRowGetsEmpty;

    private void Awake()
    {
        foreach (var brick in rowOfBricks)
        {
            brick.onBrickGetDestroyed += Brick_onBrickGetDestroyed;
        }
    }

    private void OnDestroy()
    {
        foreach (var brick in rowOfBricks)
        {
            brick.onBrickGetDestroyed -= Brick_onBrickGetDestroyed;
        }
    }

    private void Brick_onBrickGetDestroyed(object sender, Brick e)
    {
        if (rowOfBricks.Count > 0)
        {
            rowOfBricks.Remove(e);

            if (rowOfBricks.Count <= 0)
            {
                onBricksOnTheRowGetsEmpty?.Invoke(this, this);
            }
        }
    }
}
