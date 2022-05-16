using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleLight : MonoBehaviour
{

    public GameObject torchLight;
    private Light light;
    public bool on = false;
    // Start is called before the first frame update
    void Start()
    {
        light = torchLight.GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (on)
        {
            torchLight.SetActive(true);
        }
        else {
            torchLight.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            on = !on;
        }
    }
}
