using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPosition : MonoBehaviour
{
    public Transform target;
    public float rotationSpeed = 1f;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(target.position.x, target.position.y + 1.0f, target.position.z);
        if(Input.GetAxis("Look") > 0)
        {

            transform.Rotate(0f, rotationSpeed, 0f);
        } else if (Input.GetAxis("Look") < 0)
        {
            transform.Rotate(0f, -1 * rotationSpeed, 0f);
        }

    }
}
