using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderJustRun : MonoBehaviour
{

    public float xDir;
    public Rigidbody2D rb;
    public Animator anim;
    public SpiderLeg_IK legs;
    public float runSpeed;
    public float walkSpeed;
    public float radius;
    public Vector3 offset;
    public LayerMask playerLayer;

    public bool killedPlayer;
    public Transform stoppingPoint;
    bool reachedEnd;

    public bool walk;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (killedPlayer == false)
        {

            if (Vector3.Distance(transform.position, stoppingPoint.position) > 1)
            {
                if (walk == false)
                {
                    RUN();
                }
                else
                {
                    WALK();
                }

            }
            else
            {
                reachedEnd = true;

            }
            if (reachedEnd == true)
            {
                STOP();
            }

            Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position + offset, radius, playerLayer);

            if (colls.Length > 0)
            {
                killedPlayer = true;
                rb.velocity = Vector3.zero;
                anim.SetBool("IsRun", false);
                colls[0].GetComponent<PlayerController>().canMove = false;
            }
        }
    }
    void STOP()
    {
        anim.SetBool("IsRun", false);
        legs.legMoveSpeed = 10;
        Vector3 movement = new Vector3(-xDir, 0, 0).normalized;
        rb.velocity = new Vector3(movement.x * walkSpeed, rb.velocity.y, 0);
    }
    void RUN()
    {
        anim.SetBool("IsRun", true);
        legs.legMoveSpeed = 20;
        Vector3 movement = new Vector3(xDir, 0, 0).normalized;
        rb.velocity = new Vector3(movement.x * runSpeed, rb.velocity.y, 0);

    }

    void WALK()
    {
        anim.SetBool("IsRun", true);
        legs.legMoveSpeed = 10;
        Vector3 movement = new Vector3(xDir, 0, 0).normalized;
        rb.velocity = new Vector3(movement.x * walkSpeed, rb.velocity.y, 0);
    }

    void OnDrawGizmosSelected()
    {
        // Draw a semitransparent red cube at the transforms position
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawWireSphere(transform.position + offset, radius);
    }
}
