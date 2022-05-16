using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClampText : MonoBehaviour
{
    public TextMeshProUGUI label;
    public float xOffset = 0f;
    public float yOffset = 0f;
    public bool isTriggered = false;
    
    // Update is called once per frame
    void Update()
    {
        Vector3 labelPos = Camera.main.WorldToScreenPoint(this.transform.position + new Vector3(0,yOffset,0)) + new Vector3(xOffset,0,0);
        label.transform.position = labelPos;
    }

    void OnTriggerEnter(Collider other)
    {
        if(isTriggered && other.CompareTag("Player"))
        {
            Enable();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(isTriggered && other.CompareTag("Player"))
        {
            Disable();
        }
    }
    public void Enable()
    {
        label.gameObject.SetActive(true);
    }

    public void Disable()
    {
        label.gameObject.SetActive(false);
    }
}
