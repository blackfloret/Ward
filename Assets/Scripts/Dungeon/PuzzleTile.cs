using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTile : MonoBehaviour
{
    public Material matOn;
    public Material matOff;
    public PuzzleTile[] adjacentTiles;
    public bool activated = false;
    private bool solved = false;
   
    private Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        if(activated)
        {
            rend.material = matOn;
        } else
        {
            rend.material = matOff;
        }
    }


    public void toggle()
    {
        if(solved)
        {
            return;
        }
        if(activated)
        {
            rend.material = matOff;
            activated = false;
        } else
        {
            rend.material = matOn;
            activated = true;
        }
    }

    public bool isOn()
    {
        return activated;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            toggle();
            foreach(PuzzleTile tile in adjacentTiles)
            {
                tile.toggle();
            }
        }
    }

    public void solve()
    {
        solved = true;
    }
}
