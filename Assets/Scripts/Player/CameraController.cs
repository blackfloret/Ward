using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;
    private Vector3 oneVector = new Vector3(-1, 1.25f, -1);
    private CinemachineTransposer transposer;
    // Start is called before the first frame update
    void Start()
    {
        transposer = vcam.GetCinemachineComponent<CinemachineTransposer>();
        transposer.m_FollowOffset = new Vector3(-6, 6.5f, -6);

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0f && transposer.m_FollowOffset.x > -10)
        {
            transposer.m_FollowOffset = transposer.m_FollowOffset + oneVector;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0f && transposer.m_FollowOffset.x < -1)
        {
            transposer.m_FollowOffset = transposer.m_FollowOffset - oneVector;
        }
    }
}