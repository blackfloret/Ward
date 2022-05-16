using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alarm : MonoBehaviour
{

    public Rat[] rats;
    public Door[] doors;


    // Update is called once per frame
    void Update()
    {
        foreach(Rat rat in rats)
        {
            if(rat.isChasing())
            {
                foreach(Door door in doors)
                {
                    door.close();
                }
                return;
            }
        }
        foreach(Door door in doors)
        {
            door.open();
        }
    }
}
