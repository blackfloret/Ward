using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalCaveDoor : MonoBehaviour
{
    // Start is called before the first frame update
    public bool openOnStart = false;
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("open", openOnStart);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                anim.SetBool("open", true);
            }
        }
    }
}
