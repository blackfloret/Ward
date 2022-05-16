using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Start is called before the first frame update
    public bool openOnStart = false;
    public bool locked = true;
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("open", openOnStart);
    }
    
    public void open()
    {
        anim.SetBool("open", true);
    }

    public void close()
    {
        anim.SetBool("open", false);
    }

    public void playerOpen()
    {
        if(!locked)
        {
            anim.SetBool("open", true);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                playerOpen();
            }
        }
    }

    public void unlock()
    {
        locked = false;
    }
    public bool isOpen()
    {
        return anim.GetBool("open");
    }
}
