using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorPuzzle : MonoBehaviour
{
    public PuzzleTile[] puzzleTiles;
    public Door door;
    private bool solved = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(solved)
        {
            return;
        }
        foreach (PuzzleTile tile in puzzleTiles)
        {
            if(!tile.isOn())
            {
                return;
            }
        }
        foreach (PuzzleTile tile in puzzleTiles)
        {
            tile.solve();
        }
        solved = true;
        door.open();
    }
}
