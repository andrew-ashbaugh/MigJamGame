using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SpiderCreatureAI : MonoBehaviour
{
    public float radius;
    public Vector3 offset;
    public bool canSeePlayer;
    public LayerMask playerLayer;

    public Transform targetLocation;

    public float patrolSpeed;
    public float runSpeed;
    private float speed;
    public float stoppingDistance;
    public Rigidbody2D rb;
    public Animator anim;
    public SpiderLeg_IK legs;
    public float searchForPlayerTime;
  

    float searchTimer;
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position + offset, radius, playerLayer);

        if (colls.Length > 0)
        {
            canSeePlayer = true;
            targetLocation.position = colls[0].transform.position;
            speed = runSpeed;
            searchTimer =0;
        }
        else
        {
            canSeePlayer = false;
        }


        float x;
        if (targetLocation.position.x > transform.position.x)
        {
            x = 1;
        }
        else
        {
            x = -1;
        }
        Vector3 movement = new Vector3(x, 0, 0).normalized;

        rb.velocity = new Vector3(movement.x * speed, rb.velocity.y, 0);

        if (canSeePlayer == false)
        {
            // go to last known point
            // timer
            searchTimer += Time.fixedDeltaTime;

            if(searchTimer < searchForPlayerTime)
            {
                if (Vector3.Distance(transform.position, targetLocation.position) < stoppingDistance)
                {
                    // get viable point
                }

            }
            
            // choose points close

            // timer
            // patrol
        }
    }

    void Update()
    {
       

        if (speed == runSpeed)
        {
            anim.SetBool("IsRun",true);
            legs.legMoveSpeed = 20;
        }
        else
        {
            anim.SetBool("IsRun",false);
            legs.legMoveSpeed = 10;
        }


    }

    void GetPatrolPoint()
    {
        
    }



    void OnDrawGizmosSelected()
    {
        // Draw a semitransparent red cube at the transforms position
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawWireSphere(transform.position + offset, radius);
    }
}
