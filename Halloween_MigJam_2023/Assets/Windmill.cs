using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Windmill : MonoBehaviour
{
    public bool firstTouch;
    public Transform target;

    public Animator fade;
    public GameObject canvas;
    public Vector3 offset;
    public float radius;
    public LayerMask playerLayer;
    public Transform player;

    void Update()
    {
        if (canvas.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                fade.SetTrigger("FadeOut");
                player.position = target.position;
            }
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position + offset, radius, playerLayer);

        if (colls.Length > 0 && firstTouch == false)
        {
            canvas.SetActive(true);
            player = colls[0].transform;
        }
        else
        {
            canvas.SetActive(false);
        }
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && firstTouch == true)
        {
            fade.SetTrigger("FadeOut");
            other.gameObject.transform.position = target.position;
            firstTouch = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a semitransparent red cube at the transforms position
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawWireSphere(transform.position + offset, radius);
    }
}
