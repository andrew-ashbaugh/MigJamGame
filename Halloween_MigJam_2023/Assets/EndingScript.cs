using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingScript : MonoBehaviour
{
    public Vector3 offset;
    public float radius;
    public LayerMask playerLayer;
    public bool playAnimation;
    public GameObject ending1;
    public GameObject ending2;
    public GameObject ending3;
    public GameObject ending4;
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
            if(playAnimation == false)
            {
                PlayerController pc = colls[0].GetComponent<PlayerController>();
                if(pc.hasHolyWater && pc.hasDogToy)
                {
                    // dog attack, kill creature
                    ending1.SetActive(true);
                }
                else if (pc.hasHolyWater == true)
                {
                    // kill creature only anim
                    ending2.SetActive(true);
                }
                else if(pc.hasDogToy == true)
                {
                    // dog sacrifices himself
                    ending3.SetActive(true);
                }
                else
                {
                    // player dies ending
                    ending4.SetActive(true);
                }
                playAnimation = true;
                gameObject.SetActive(false);
                colls[0].gameObject.SetActive(false);

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
