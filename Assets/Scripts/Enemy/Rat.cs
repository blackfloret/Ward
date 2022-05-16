using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Rat : MonoBehaviour
{
    public Player player;
    private Vector3 playerBodyOffset;

    Animator anim;
    public float walkSpeed = 3.0f;
    public float runSpeed = 5.0f;

    // Pathing and Combat AI
    NavMeshAgent agent;
    Vector3 lastSeen;
    int destPoint = 0;
    public bool playerInSight, chasing, attacking, canHurt;
    public Transform[] points;
    public Transform eyeLine;
    public int damage = -5;
    RaycastHit hit;

    AudioSource audioSource;
    [SerializeField]
    AudioClip swordHit;
    [SerializeField]
    AudioClip yell;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        agent.autoBraking = playerInSight = chasing = attacking = canHurt = false;
    }
    
    void Update()
    {
        
        if (Vector3.Distance(transform.position, player.transform.position) < 15)
        {
            Debug.DrawRay(eyeLine.position, new Vector3(player.transform.position.x, player.transform.position.y + 1, player.transform.position.z) - eyeLine.position, Color.green);
            if (Physics.Raycast(eyeLine.position, new Vector3(player.transform.position.x, player.transform.position.y + 1,player.transform.position.z) - eyeLine.position, out hit) && Vector3.Angle(eyeLine.forward, (player.transform.position - eyeLine.transform.position)) < 45)
            {
                if (hit.collider.CompareTag("Player"))
                {
                    playerInSight = true;
                    if(chasing == false)
                    {
                        audioSource.PlayOneShot(yell);
                    }
                    chasing = true;
                    lastSeen = player.transform.position;
                }
                else
                {
                    playerInSight = false;
                }
            }
            else
            {
                playerInSight = false;
            }
        } else
        {
            playerInSight = false;
        }

        if (!player.alive)
        {
            playerInSight = false;
            chasing = false;
        }
        if(!agent.pathPending)
        {
            if (!playerInSight && chasing)
            {
                if (Vector3.Distance(transform.position, player.transform.position) < 2)
                {
                    agent.destination = player.transform.position;
                }
                else if (agent.remainingDistance < 0.5f)
                {
                    print("Resuming patrol");
                    agent.destination = points[destPoint].position;
                    chasing = false;
                }
            }
            else if (playerInSight)
            {
                agent.destination = lastSeen;
                if (Vector3.Distance(transform.position, player.transform.position) < 2.5f && !attacking)
                {
                    anim.Play("Attack.rat_attack");
                    canHurt = true;
                }
            }
            else if (agent.remainingDistance < 0.5f)
            {
                GotoNextPoint();
            }
        }

        attacking = anim.GetCurrentAnimatorStateInfo(1).IsName("Attack.rat_attack");


        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.rat_yell"))
        {
            agent.speed = 0f;
        }
        else if (attacking)
        {
            agent.speed = 0f;
            attacking = true;
        } else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.rat_idle_look"))
        {
            agent.speed = 0f;
        }
        else if (chasing)
        {
            agent.speed = runSpeed;
        }
        else
        {
            agent.speed = walkSpeed;
        }

        anim.SetFloat("velocity", agent.velocity.sqrMagnitude);
        anim.SetBool("chasing", chasing);
    }

    void GotoNextPoint()
    {
        if (points.Length == 0)
            return;

        agent.destination = points[destPoint].position;
        destPoint = (destPoint + 1) % points.Length;
    }

    void OnTriggerEnter(Collider other)
    {
        if (attacking & other.CompareTag("Player") && canHurt)
        {
            if(player.blocking && Vector3.Angle(player.transform.forward, transform.position - player.transform.position) < 45)
            {
                player.adjustHealth(0);
                player.rb.velocity = transform.forward * 2f;
            } else
            {
                player.adjustHealth(damage);
                audioSource.PlayOneShot(swordHit, 0.5f);
            }
            canHurt = false;
        }
    }

    public bool isChasing()
    {
        return chasing;
    }

}
