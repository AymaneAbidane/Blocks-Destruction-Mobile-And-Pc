using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bricks : MonoBehaviour
{
    [SerializeField, ChildGameObjectsOnly] private List<RowOfBricks> allRowsOfBricks = new();

    public event EventHandler onAllBricksGetsDestroyed;
    private void Awake()
    {
        foreach (var row in allRowsOfBricks)
        {
            row.onBricksOnTheRowGetsEmpty += Row_onBricksOnTheRowGetsEmpty; ;
        }
    }

    private void OnDestroy()
    {
        foreach (var row in allRowsOfBricks)
        {
            row.onBricksOnTheRowGetsEmpty -= Row_onBricksOnTheRowGetsEmpty; ;
        }
    }

    private void Row_onBricksOnTheRowGetsEmpty(object sender, RowOfBricks e)
    {
        if (allRowsOfBricks.Count > 0)
        {
            allRowsOfBricks.Remove(e);

            if (allRowsOfBricks.Count <= 0)
            {
                onAllBricksGetsDestroyed?.Invoke(this, null);
            }
        }
    }


}
