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
    public Animator fadePane;
    public AudioSource playerDeathNoise;
    public CornChase cc;
    public Transform respawnPoint;
    public Transform player;

    float deathTimer;
    public SpiderLeg_IK spiderIK;
    public Transform[] playerKillTargs;
    public float legRunSpeed = 20;
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
                player = colls[0].transform;
                player.GetComponent<PlayerController>().canMove = false;
                player.GetComponent<PlayerController>().anim.SetBool("IsDead",true);
                fadePane.SetTrigger("FadeOut");
                playerDeathNoise.pitch = Random.Range(0.95f,1.05f);
                playerDeathNoise.Play();
                AssignKillTarg();
                spiderIK.KillPlayer();
            }
        }
        else
        {
            deathTimer += Time.fixedDeltaTime;
            if (deathTimer >= 1)
            {
                player.position = respawnPoint.position;
                ResetChase(player);
                deathTimer = 0;
            }
        }
    }
    void STOP()
    {
        anim.SetBool("IsRun", false);
        legs.legMoveSpeed = 10;
        Vector3 movement = new Vector3(0, 0, 0).normalized;
        rb.velocity = new Vector3(movement.x * walkSpeed, rb.velocity.y, 0);
    }
    void RUN()
    {
        anim.SetBool("IsRun", true);
        legs.legMoveSpeed = legRunSpeed;
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

    public void ResetChase(Transform player)
    {
        spiderIK.killPlayer = false;
        player.GetComponent<PlayerController>().anim.SetBool("IsDead", false);
        cc.ResetChase();
        killedPlayer = false;
        player.GetComponent<PlayerController>().canMove = true;
        rb.velocity = Vector3.zero;
        anim.SetBool("IsRun", false);
        reachedEnd = false;

    }

    public void AssignKillTarg()
    {
        if (spiderIK.transform.position.x > player.transform.position.x)
        {
            // player to the left of spider
            if (player.GetComponent<PlayerController>().anim.transform.localScale.x < 0)
            {
                // facing left
                spiderIK.killTarg = playerKillTargs[0];

            }
            else
            {
                //facing right
                spiderIK.killTarg = playerKillTargs[1];
            }
        }
        else
        {
            // player to the right of spider
            if (player.GetComponent<PlayerController>().anim.transform.localScale.x < 0)
            {
                // facing left
                spiderIK.killTarg = playerKillTargs[1];

            }
            else
            {
                //facing right
                spiderIK.killTarg = playerKillTargs[0];
            }
        }
    }

}
