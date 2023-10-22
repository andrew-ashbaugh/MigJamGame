using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Well : MonoBehaviour
{
    public bool firstEncounter;
    public GameObject itemList;

    public GameObject needRope;
    public GameObject needBucket;

    public GameObject haveRope;
    public GameObject haveBucket;

    public AudioSource dogBarking;
    public GameObject firstDialog;
    public GameObject canvas;
    public Vector3 offset;
    public float radius;
    public LayerMask playerLayer;

    float firstEncounterTimer;
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
            canvas.SetActive(true);
            if (firstEncounter == true)
            {

                firstDialog.SetActive(true);
                firstEncounter = false;
                colls[0].GetComponent<PlayerController>().canMove = false;
            }
            if (firstEncounter == false)
            {
                if (firstEncounterTimer < 5)
                {
                    firstEncounterTimer += Time.fixedDeltaTime;
                }
            }

            if (firstEncounter == false)
            {
                if (firstEncounterTimer > 5)
                {
                    if (dogBarking.isPlaying)
                    {
                        dogBarking.Stop();
                        colls[0].GetComponent<PlayerController>().canMove = true;
                    }

                    CheckItems(colls[0].GetComponent<PlayerController>());
                    firstDialog.SetActive(false);
                }

            }

        }
        else
        {
            canvas.SetActive(false);
        }


    }

    void CheckItems(PlayerController pc)
    {
        if (pc.hasRope == true)
        {
            haveRope.SetActive(true);
        }
        else
        {
            haveRope.SetActive(false);
        }

        if (pc.hasBucket == true)
        {
            haveBucket.SetActive(true);
        }
        else
        {
            haveBucket.SetActive(false);
        }

        if (haveBucket.activeSelf == true && haveRope.activeSelf == true)
        {
            itemList.SetActive(false);
            // do something
        }
        else
        {
            itemList.SetActive(true);
        }
    }
    void OnDrawGizmosSelected()
    {
        // Draw a semitransparent red cube at the transforms position
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawWireSphere(transform.position + offset, radius);
    }

}
