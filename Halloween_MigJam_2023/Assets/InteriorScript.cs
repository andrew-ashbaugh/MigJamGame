using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteriorScript : MonoBehaviour
{
    public bool inside;
    public SpriteRenderer outsideSprite;
    public GameObject insideSprite;

    public Vector3 offset;
    public Vector3 size;
    public LayerMask playerLayer;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] colls = Physics2D.OverlapBoxAll(transform.position + offset, size, 0, playerLayer);

        if (colls.Length > 0)
        {
            inside = true;
        }
        else
        {
            inside = false;
        }

    }

    private void FixedUpdate() 
    {
        if (inside == true)
        {
            insideSprite.SetActive(true);
            if (outsideSprite.color.a > 0)
            {
                outsideSprite.color = new Color(outsideSprite.color.r, outsideSprite.color.g, outsideSprite.color.b,
                outsideSprite.color.a - speed);
            }
        }
        else
        {

            if (outsideSprite.color.a < 1)
            {
                outsideSprite.color = new Color(outsideSprite.color.r, outsideSprite.color.g, outsideSprite.color.b,
                outsideSprite.color.a + speed);
            }
            else
            {
                insideSprite.SetActive(false);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a semitransparent red cube at the transforms position
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawWireCube(transform.position + offset, size);
    }
}
