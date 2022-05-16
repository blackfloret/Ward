using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathTiles : MonoBehaviour
{
    public Material matOn;
    public Material matOff;
    public bool initialState = false;
    private bool activated;
    private bool failed = false;
    private bool solved = false;

    private Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        activated = initialState;
        if (activated)
        {
            rend.material = matOn;
        }
        else
        {
            rend.material = matOff;
        }
    }


    public void toggle()
    {
        if (solved)
        {
            return;
        }
        if (activated)
        {
            rend.material = matOff;
            activated = false;
        }
        else
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
        if (other.gameObject.tag == "Player" && !activated)
        {
            toggle();
        } else if(other.gameObject.tag == "Player" && activated)
        {
            failed = true;
        }
    }

    public void solve()
    {
        solved = true;
    }

    public bool isFailed()
    {
        return failed;
    }

    public void reset()
    {
        activated = initialState;
        failed = false;
        if (activated)
        {
            rend.material = matOn;
        }
        else
        {
            rend.material = matOff;
        }
    }
}
