using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornChase : MonoBehaviour
{
    public Transform startPos;
    public Vector3 offset;
    public float radius;
    public LayerMask playerLayer;

    public GameObject spider;
    bool startedChase;
    Vector3 spiderStartPoint;
    public GameObject spiderBody;
    public Transform[] spiderLegTargs;

    // Start is called before the first frame update
    void Start()
    {
        spiderStartPoint = spiderBody.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Collider2D[] colls = Physics2D.OverlapCircleAll(startPos.position + offset, radius, playerLayer);

        if (colls.Length > 0)
        {
            if (startedChase == false)
            {
                startedChase = true;
                StartChase();
            }
        }
    }

    void StartChase()
    {
        spider.SetActive(true);
    }

    void OnDrawGizmosSelected()
    {
        // Draw a semitransparent red cube at the transforms position
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawWireSphere(startPos.position + offset, radius);
    }

    public void ResetChase()
    {
        spiderBody.transform.position = spiderStartPoint;
        spider.SetActive(false);
        startedChase = false;

        spiderLegTargs[0].position = spiderStartPoint;
        spiderLegTargs[1].position = spiderStartPoint;
        
    }
}
