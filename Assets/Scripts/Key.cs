using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{

    public Door door;
    private ClampText clampText;

    void Start()
    {
        clampText = GetComponent<ClampText>();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            clampText.Enable();
        }
    }
    // Update is called once per frame
    void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                door.unlock();
                clampText.Disable();
                Destroy(this.gameObject);
            }
        }
    }
}
