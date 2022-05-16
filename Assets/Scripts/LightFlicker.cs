using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public float intensity = 1f;
    private Light light;
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (light.intensity > intensity)
        {
            light.intensity = intensity + Random.Range(-0.2f, 0.05f);
        }
        else
        {
            light.intensity = intensity + Random.Range(-0.05f, 0.2f);
        }
    }
}
