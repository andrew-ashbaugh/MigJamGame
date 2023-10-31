using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnScript : MonoBehaviour
{
    public Transform respawnPoint;

    public SpiderJustRun chaseReset;

    public float respawnTime;
    public Animator fadePane;

    float respawnTimer;
    bool isDead;

    Transform player;
    Animator playerAnim;

    private void FixedUpdate()
    {
        if (isDead == true)
        {
            respawnTimer += Time.fixedDeltaTime;
            if(respawnTimer>=respawnTime)
            {
                isDead = false;
                respawnTimer = 0;
                player.transform.position = respawnPoint.position;
                playerAnim.SetBool("IsDead", false);
                player.GetComponent<PlayerController>().canMove = true;

                if (chaseReset!=null)
                {
                    chaseReset.ResetChase(player);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isDead = true;
            player = other.gameObject.transform;
            playerAnim = player.gameObject.GetComponent<PlayerController>().anim;
            playerAnim.SetBool("IsDead", true);
            player.gameObject.GetComponent<PlayerController>().canMove = false;
            fadePane.SetTrigger("FadeOut");
        }
    }
}
