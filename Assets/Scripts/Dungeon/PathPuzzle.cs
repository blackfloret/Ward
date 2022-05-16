using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPuzzle : MonoBehaviour
{
    public PathTiles[] pathTiles;
    public Door door;
    private bool solved = false;


    // Update is called once per frame
    void Update()
    {
        if (solved)
        {
            return;
        }
        foreach (PathTiles tile in pathTiles)
        {
            if (tile.isFailed())
            {
                foreach(PathTiles resetTile in pathTiles)
                {
                    resetTile.reset();
                }
                return;
            }
        }
        foreach (PathTiles tile in pathTiles)
        {
            if (!tile.isOn())
            {
                return;
            }
        }
        foreach (PathTiles tile in pathTiles)
        {
            tile.solve();
        }
        solved = true;
        door.open();
    }
}
