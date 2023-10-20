using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoliageInteractor : MonoBehaviour
{

    public Vector3 offset;
    public float radius;
    public LayerMask foliageLayer;

    public List<Transform> foliage;
    public float bendThreshold;
    public float raiseDuration = 0.1f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position + offset, radius, foliageLayer);

        if (colls.Length > 0)
        {
            for (int i = 0; i < colls.Length; i++)
            {
                if (!foliage.Contains(colls[i].transform))
                {
                    foliage.Add(colls[i].transform);
                }
            }

        }

        for (int j = 0; j < foliage.Count; j++)
        {
            if (Vector3.Distance(foliage[j].position, transform.position) > (radius * 2))
            {
                if (foliage[j].transform.localRotation.eulerAngles.z > -2 && 
                foliage[j].transform.localRotation.eulerAngles.z < 2f)
                {
                    foliage.Remove(foliage[j]);
                }
                else
                {
                    StartCoroutine(AnimateRotationTowards(foliage[j], foliage[j].parent.rotation, raiseDuration));
                }
            }
            else
            {
                if (foliage[j].position.x > transform.position.x)
                {
                    foliage[j].right = (foliage[j].transform.position - transform.position);
                    foliage[j].transform.rotation = Quaternion.Euler(
                    foliage[j].transform.rotation.eulerAngles.x,
                    foliage[j].transform.rotation.eulerAngles.y,
                    foliage[j].transform.rotation.eulerAngles.z - bendThreshold);
                }
                else
                {
                    foliage[j].right = (transform.position - foliage[j].transform.position);
                    foliage[j].transform.rotation = Quaternion.Euler(
                    foliage[j].transform.rotation.eulerAngles.x,
                    foliage[j].transform.rotation.eulerAngles.y,
                    foliage[j].transform.rotation.eulerAngles.z + bendThreshold);
                }
               
            }


        }

    }

    private System.Collections.IEnumerator AnimateRotationTowards(Transform target, Quaternion rot, float dur)
    {
        float t = 0f;
        Quaternion start = target.rotation;
        while (t < dur)
        {
            target.rotation = Quaternion.Slerp(start, rot, t / dur);
            yield return null;
            t += Time.deltaTime;
        }
        target.rotation = rot;
    }
    void OnDrawGizmosSelected()
    {
        // Draw a semitransparent red cube at the transforms position
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawWireSphere(transform.position + offset, radius);
    }

}

