using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : MonoBehaviour
{
    private bool canHurt = true;
    private bool growing = true;
    private Vector3 growth;
    private Vector3 direction;
    public float growthRate = 0.025f;
    public float velocity = 100f;
    public int damage = -5;

    AudioSource audioSource;
    [SerializeField]
    AudioClip hit;

    void Awake()
    {
        this.transform.localScale = new Vector3(0.11f, 0.11f, 0.11f);
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = hit;
        growth = new Vector3(growthRate, growthRate, growthRate);
    }

    void Update()
    {
        if(this.transform.localScale.x < 1 && growing)
        {
            this.transform.localScale += growth;
        } else if(this.transform.localScale.x > 0 && !growing)
        {
            this.transform.localScale -= (2 * growth);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && canHurt)
        {
            Player player = other.gameObject.GetComponent<Player>();
            Vector3 projectileAngle = transform.position - player.transform.position;
            projectileAngle.y = 0;
            if (player.blocking && Vector3.Angle(player.transform.forward, direction * -1) < 45)
            {
                player.adjustHealth(0);
            }
            else
            {
                player.adjustHealth(damage);
                audioSource.Play();
            }
        }
        canHurt = false;
        Destroy(this.gameObject, 3);
        growing = false;
    }

    public void Launch(Vector3 direction)
    {
        this.direction = Vector3.Scale(direction, new Vector3(1,0,1));
        this.GetComponent<Rigidbody>().AddForce(direction * velocity);
    }
}
