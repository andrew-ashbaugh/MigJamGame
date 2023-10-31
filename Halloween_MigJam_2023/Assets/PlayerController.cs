using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator anim;
    public float moveSpeed;

    public GameObject diary;

    Vector3 movement;
    Rigidbody2D rb;
    Vector3 spriteScale;

    public bool hasRope;
    public bool hasBucket;
    public bool canMove;


    [SerializeField] private bool isGrounded;
    [SerializeField] private Transform groundRayTransform;
    [SerializeField] private Transform groundRay2Transform;
    [SerializeField] private Transform groundRay3Transform;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private int numJumps;
    [SerializeField] private int jumpForce;
    public GameObject lampIk;
    public GameObject lamp;
    private int jumpsLeft;
    private float jumpTimer;

    public bool hasHolyWater;
    public bool hasDogToy;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        spriteScale = anim.transform.localScale;
    }

    void FixedUpdate()
    {
        bool grounded1 = Physics2D.Linecast(transform.position, groundRayTransform.position, groundLayer);
        bool grounded2 = Physics2D.Linecast(transform.position, groundRay2Transform.position, groundLayer);
        bool grounded3 = Physics2D.Linecast(transform.position, groundRay3Transform.position, groundLayer);

        if (grounded1 == true || grounded2 == true || grounded3 == true)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if (isGrounded == true && jumpTimer >= 0.5f)
        {
            jumpsLeft = numJumps;
            anim.SetBool("IsGrounded", true);
            // fallTimer =0;
        }
        if (isGrounded == false && jumpTimer >= 0.1f)
        {
            anim.SetBool("IsGrounded", false);

        }

        jumpTimer += Time.fixedDeltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove == true)
        {
            float x = Input.GetAxisRaw("Horizontal");
            movement = new Vector3(x, 0, 0).normalized;
            anim.SetBool("IsWalk", x != 0);
            rb.velocity = new Vector3(movement.x * moveSpeed, rb.velocity.y, 0);

            if (diary.activeSelf == true)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;

                if (x != 0 && x < 0)
                {
                    anim.transform.localScale = new Vector3(-spriteScale.x, spriteScale.y, spriteScale.z);
                }
                if (x != 0 && x > 0)
                {
                    anim.transform.localScale = new Vector3(spriteScale.x, spriteScale.y, spriteScale.z);
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (jumpsLeft > 0)
                {

                    jumpsLeft--;
                    jumpTimer = 0;
                    anim.SetBool("IsWalk", false);
                    anim.SetTrigger("IsJump");
                }
            }
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            anim.SetBool("IsWalk", false);
        }


    }

    public void ApplyJumpForce()
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.up * jumpForce);
    }

    public void GiveLamp()
    {
        lampIk.SetActive(true);
        lamp.SetActive(true);
    }

}
