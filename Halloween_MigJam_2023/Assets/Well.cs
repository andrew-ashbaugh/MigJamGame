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

    public GameObject[] wellSprites;
    public GameObject ePrompt;
    public bool canGoDown;
    public bool goingDown;
    public Animator well;
    PlayerController pc;
    // Start is called before the first frame update
    void Start()
    {

    }


    void Update()
    {
        if (canGoDown == true && goingDown == false)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                goingDown = true;
                Debug.Log("GO DOWN WELL");
                itemList.SetActive(false);
                wellSprites[0].SetActive(false);
                wellSprites[1].SetActive(true);
                ePrompt.SetActive(false);
                well.SetBool("Descend",true);
                pc.canMove = false;

                // Switch scenes via animation helper -> well completed -> GoDownWell()
            }
        }
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
                        pc = colls[0].GetComponent<PlayerController>();
                    }

                    if (colls[0].GetComponent<PlayerController>().hasBucket && colls[0].GetComponent<PlayerController>().hasRope)
                    {
                        if (goingDown == false)
                        {
                            CheckItems(colls[0].GetComponent<PlayerController>());
                            ePrompt.SetActive(true);
                            canGoDown = true;
                        }

                    }
                    else
                    {
                        CheckItems(colls[0].GetComponent<PlayerController>());
                        
                        firstDialog.SetActive(false);
                        canGoDown = false;
                    }
                }

            }



        }
        else
        {
            canGoDown = false;
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
            // itemList.SetActive(false);
            // do something
            if (goingDown == false)
            {
                itemList.SetActive(true);
            }

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
