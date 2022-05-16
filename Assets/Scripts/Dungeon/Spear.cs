using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    public int damage = -5;

    AudioSource audioSource;
    [SerializeField]
    AudioClip hit;
    // Start is called before the first frame update
    void Start()
    {
        //switch
    }

    // Update is called once per frame
    void Update()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = hit;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();
            Vector3 projectileAngle = transform.position - player.transform.position;
            projectileAngle.y = 0;
            if (player.blocking && Vector3.Angle(player.transform.forward, projectileAngle) < 45)
            {
                player.adjustHealth(0);
            }
            else
            {
                player.adjustHealth(damage);
                audioSource.Play();
            }
        }
    }
}
