using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Rigidbody rb;
    public Transform t;
    public CapsuleCollider c;
    public Animator anim;
    public GameObject timer;
    
    public float initialJumpHeight;
    private float jumpHeight;

    private float maxSpeed;
    public float initialMaxSpeed;
    public float movingThreshold = 0.2f;

    private bool facingRight = true;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        t = GetComponent<Transform>();
        c = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        //animator stuff
        anim.SetBool("Grounded", isGrounded());
        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.J)) && isGrounded())
        {
            anim.SetBool("Sprinting", true);
            maxSpeed = 2 * initialMaxSpeed;
            jumpHeight = initialJumpHeight + 2;
        }
        else if (isGrounded())
        {
            anim.SetBool("Sprinting", false);
            maxSpeed = initialMaxSpeed;
            jumpHeight = initialJumpHeight;
        }

        if (-movingThreshold < rb.velocity.x && rb.velocity.x < movingThreshold)
        {
            anim.SetBool("Moving", false);
        }
        else
        {
            anim.SetBool("Moving", true);
        }
        
        //flip mario
        if ((rb.velocity.x > 0.25 && !facingRight) || (rb.velocity.x < -0.25 && facingRight))
        {
            t.Rotate(Vector3.down * 180);
            facingRight = !facingRight;
        }   
    }

    void FixedUpdate()
    {

        //rb.AddForce(Vector3.right * Input.GetAxis("Horizontal") * moveSpeed);
        rb.velocity = new Vector3(maxSpeed * Input.GetAxis("Horizontal"), rb.velocity.y,0);
        if (isGrounded() && (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.K)))
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpHeight, 0);
        }

        if (rb.velocity.x > maxSpeed)
        {
            rb.velocity = new Vector3(maxSpeed, rb.velocity.y, rb.velocity.z);
        } else if (rb.velocity.x < -maxSpeed)
        {
            rb.velocity = new Vector3(-maxSpeed, rb.velocity.y, rb.velocity.z);
        }
    }

    private bool isGrounded()
    {
        Ray r = new Ray(c.bounds.center, Vector3.down);
        return Physics.Raycast(r, c.bounds.extents.y + 0.25f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Ray r = new Ray(c.bounds.center, Vector3.up);
        if (Physics.Raycast(r, c.bounds.extents.y + 0.25f))
        {
            if (collision.gameObject.name == "brick(Clone)")
            {
                Destroy(collision.gameObject);
                GameObject.Find("ScoreText").GetComponent<ScoreText>().addScore(100);
            } else if ((collision.gameObject.name == "qBlock(Clone)"))
            {
                GameObject.Find("CoinsText").GetComponent<CoinsText>().Increment();
                GameObject.Find("ScoreText").GetComponent<ScoreText>().addScore(100);
                collision.gameObject.GetComponent<MeshRenderer>().material.mainTextureOffset += new Vector2(0, 0.2f);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Water(Clone)")
        {
            killMario();
        } else if (other.name == "Goal(Clone)")
        {
            Debug.Log("You Win!");
            timer.GetComponent<TimeScript>().stopCountdown();
        }
    }

    public void killMario()
    {
        Debug.Log("Mario has deceased.");
        timer.GetComponent<TimeScript>().stopCountdown();
    }
}
