using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectiles : MonoBehaviour
{
    public GameObject projectile;
    public GameObject spawnPoint;
    public float fireRate = 1.0f;
    public Transform top;
    private GameObject target;

    [SerializeField]
    private AudioClip _gunShot;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if(audioSource == null)
        {
            Debug.LogError("The AudioSource in the player NULL!");
        } else
        {
            audioSource.clip = _gunShot;
        }
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 targetDirection = Vector3.Scale((target.transform.position - transform.position), new Vector3(1, 0, 1));
            top.rotation = Quaternion.LookRotation(targetDirection);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            InvokeRepeating("LaunchProjectile", 0f, fireRate);
            Debug.Log("Targeting: " + other.name);
            target = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == target)
        {
            CancelInvoke();
            target = null;
        }
    }

    void LaunchProjectile()
    {
        if(target.GetComponent<Player>().alive)
        {
            Vector3 direction = target.transform.position - spawnPoint.transform.position + new Vector3(0, 1.5f, 0);
            GameObject bullet = Instantiate(projectile, spawnPoint.transform.position, Quaternion.identity) as GameObject;
            bullet.GetComponent<Cannonball>().Launch(direction.normalized);
            audioSource.Play();
        }
    }
}
