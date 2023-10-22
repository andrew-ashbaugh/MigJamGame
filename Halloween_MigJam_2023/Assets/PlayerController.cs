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
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        spriteScale = anim.transform.localScale;
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

        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            anim.SetBool("IsWalk", false);
        }


    }

}
