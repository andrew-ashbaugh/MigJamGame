using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstLookAtCreature : MonoBehaviour
{
    public Vector3 offset;
    public float radius;
    public LayerMask playerLayer;

    public bool enableSpider;

    public GameObject passiveSpider;
    public GameObject spider;

    public float timer;
    public PlayerController pc;

    void FixedUpdate()
    {
        Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position + offset, radius, playerLayer);

        if (colls.Length > 0)
        {
            if (enableSpider == false)
            {
                enableSpider = true;
                passiveSpider.SetActive(false);
                spider.SetActive(true);
                pc = colls[0].GetComponent<PlayerController>();
                pc.canMove = false;
            }
        }

        if (enableSpider == true)
        {
            if (timer >= 5)
            {
                spider.SetActive(false);
                pc.canMove = true;
            }
            else
            {
                timer += Time.fixedDeltaTime;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a semitransparent red cube at the transforms position
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawWireSphere(transform.position + offset, radius);
    }
}
