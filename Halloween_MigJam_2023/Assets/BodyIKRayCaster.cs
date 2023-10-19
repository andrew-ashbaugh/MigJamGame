using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyIKRayCaster : MonoBehaviour
{
    public float hoverDist;
    public LayerMask groundLayer;

    public bool body;
    public float rotationSpeed;

    public Quaternion desiredRot;
    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, Vector3.down, 10, groundLayer);
        if (hit.collider != null)
        {

            Vector3 point = hit.point; // gets the position where the leg hit something
            point.y += hoverDist;
            transform.position = point;
            if (body == true)
            {
           
            desiredRot = Quaternion.FromToRotation(transform.right, hit.normal) * transform.rotation;
            }
        }

        if (body == true)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, desiredRot, Time.deltaTime * rotationSpeed);

        }

    }
}
