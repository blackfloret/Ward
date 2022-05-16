using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Ward
public class Player : MonoBehaviour
{
    public float walkSpeed = 2f;
    public float runSpeed = 5f;
    public float turnSpeed = 5f;

    public float health = 20f;
    public float maxHealth = 20f;
    public Slider healthBar;
    public bool alive = true;
    public bool blocking = false;
    public bool hasShield = false;
    public Rigidbody rb;
    public GameObject shield;

    AudioSource audioSource;
    Animator anim;
    [SerializeField]
    AudioClip[] grassSteps;
    [SerializeField]
    AudioClip[] grassRunSteps;
    int stepCount = 0;
    [SerializeField]
    AudioClip shieldBlock;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        shield.SetActive(hasShield);

        healthBar.value = health;
        healthBar.maxValue = maxHealth;
    }

    public float adjustHealth(float adjustment)
    {
        health += adjustment; //if Ward is healed
        healthBar.value = health;

        if (health < 1) { //if Ward is dead
            StartCoroutine(deathAnimation());
        } else if (adjustment < 0) { //if Ward is hurt
            anim.Play("Base Layer.WardHurt");
        } else if (adjustment == 0) { //if Ward has successfully blocked
            anim.Play("Shield.WardBlockImpact");
            audioSource.PlayOneShot(shieldBlock);
        }
        return health;
    }

    IEnumerator deathAnimation()
    {
        alive = false;
        anim.SetBool("dead", true);
        yield return new WaitForSeconds(3);
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    void FixedUpdate()
    {
        if(!alive)
        {
            return;
        }
        //rb.AddForce(transform.forward);
        Debug.DrawRay(transform.position, transform.forward * 5, Color.green);
        var camera = Camera.main;
        float moveSpeed = 0;
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        bool running = Input.GetButton("Run");
        if(hasShield)
        {
            blocking = Input.GetButton("Block");
        }
        bool moving = (moveHorizontal != 0.0f) || (moveVertical != 0.0f);
        Vector3 fwd = camera.transform.forward;
        Vector3 rt = camera.transform.right;

        fwd.y = 0f;
        rt.y = 0f;
        fwd.Normalize();
        rt.Normalize();

        Vector3 moveDirection = fwd * moveVertical + rt * moveHorizontal;
        anim.SetBool("moving", moving);
        anim.SetBool("blocking", blocking);
        anim.SetBool("running", running && !blocking);

        if(moving && running && !blocking)
        {
            moveSpeed = runSpeed;
            time += Time.deltaTime * 3.58f;
        } else if (moving && !blocking)
        {
            anim.SetFloat("walkingMultiplier", 1.0f);
            moveSpeed = walkSpeed;
            time += Time.deltaTime * 2.27f;
        } else if (moving && blocking)
        {
            anim.SetFloat("walkingMultiplier", 0.75f);
            moveSpeed = 0.75f * walkSpeed;
            time += Time.deltaTime * 1.7025f;
        }
        
        if(!moving)
        {
            anim.SetBool("moving", false);
            //rb.AddForce(transform.forward * -1 * 3);
        } else
        {
            anim.SetBool("moving", true);
            //transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
            rb.AddForce(moveDirection * moveSpeed * Time.deltaTime, ForceMode.VelocityChange);
            Quaternion newRotate = Quaternion.Euler(0, Vector3.SignedAngle(fwd, moveDirection, Vector3.up) + camera.transform.eulerAngles.y, 0);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotate, Time.deltaTime * turnSpeed * 100);
        }

        if(time >= 1f)
        {
            time = time % 1f;
            if(running)
            {
                Step(grassRunSteps);
            } else
            {

                Step(grassSteps);
            }
        }
    }

    void Step(AudioClip[] steps)
    {
        stepCount = stepCount % steps.Length;
        audioSource.PlayOneShot(steps[stepCount]);
        stepCount = stepCount + 1;
    }

    public void pickupShield()
    {
        hasShield = true;
        shield.SetActive(true);
    }

}
