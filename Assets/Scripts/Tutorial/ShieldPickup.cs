using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPickup : MonoBehaviour
{
    public Player playerScript;
    private ClampText clampText;

    void Start()
    {
        clampText = GetComponent<ClampText>();
        clampText.Disable();
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            clampText.Enable();
        }
    }
    public void OnTriggerStay(Collider other)
    {

        if(other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("E key was pressed.");
                playerScript.pickupShield();
                clampText.Disable();
                Destroy(this.gameObject);

            }
        }
    }
}
