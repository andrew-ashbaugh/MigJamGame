using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderLeg_IK : MonoBehaviour
{

    [Header("Right Leg")]
    public Transform legRTarget;
    public Transform legRBodyTarget;

    [Header("Left Leg")]
    public Transform legLTarget;
    public Transform legLBodyTarget;

    [Header("Settings")]
    public float maxLegDist;
    public float legMoveSpeed;
    public float legLiftDistance;
    public float bodyHeight;

    bool moveLToTarget;
    bool moveRToTarget;

    int LIndex;
    int RIndex;

    public bool killPlayer;
    public Transform killTarg;

    public AudioSource legAudio;
    public float legPitchMin;
    public float legPitchMax;
    // Start is called before the first frame update
    void Start()
    {

    }

    void FixedUpdate()
    {

        if (killPlayer == false)
        {
            MoveLegs();
        }
        else
        {
            KillPlayer();
        }
        



    }

    void MoveLegs()
    {
        if (Vector3.Distance(legLBodyTarget.position, legLTarget.position) > maxLegDist && moveRToTarget == false)
        {
            moveLToTarget = true;

        }
        if (Vector3.Distance(legRBodyTarget.position, legRTarget.position) > maxLegDist && moveLToTarget == false)
        {
            moveRToTarget = true;

        }
        MoveLToTarget();
        MoveRToTarget();
    }

    void MoveLToTarget()
    {
        if (moveLToTarget == true)
        {
            Vector3 movePos = legLBodyTarget.position;
            if (LIndex == 0)
            {
                movePos = Vector3.Lerp(legLTarget.position, legLBodyTarget.position, 0.5f);
                movePos.y += legLiftDistance;
            }
            else
            {
                movePos = legLBodyTarget.position;
            }
            float step = legMoveSpeed * Time.deltaTime; // calculate distance to move
            legLTarget.position = Vector3.MoveTowards(legLTarget.position, movePos, step);

            if (Vector3.Distance(legLTarget.position, movePos) <= 0.01f)
            {
                if (LIndex == 1)
                {
                    moveLToTarget = false;
                    LIndex = 0;
                    PlayLegAudio();
                }
                else
                {
                    LIndex = 1;
                  
                }

            }
        }

    }



    void MoveRToTarget()
    {
        if (moveRToTarget == true)
        {
            Vector3 movePos = legRBodyTarget.position;
            if (RIndex == 0)
            {
                movePos = Vector3.Lerp(legRTarget.position, legRBodyTarget.position, 0.5f);
                movePos.y += legLiftDistance;
            }
            else
            {
                movePos = legRBodyTarget.position;
            }
            float step = legMoveSpeed * Time.deltaTime; // calculate distance to move
            legRTarget.position = Vector3.MoveTowards(legRTarget.position, movePos, step);

            if (Vector3.Distance(legRTarget.position, movePos) <= 0.01f)
            {
                if (RIndex == 1)
                {
                    moveRToTarget = false;
                    RIndex = 0;
                    PlayLegAudio();
                }
                else
                {
                    RIndex = 1;
                   
                }

            }
        }

    }

    public void KillPlayer()
    {
        killPlayer = true;
        if (Vector3.Distance(legRTarget.position, killTarg.position) < Vector3.Distance(legLTarget.position, killTarg.position))
        {
            // right leg closer
            float step = legMoveSpeed * Time.deltaTime; // calculate distance to move
            legRTarget.position = Vector3.MoveTowards(legRTarget.position, killTarg.position, step);
        }
        else
        {
            // left leg closer
            float step = legMoveSpeed * Time.deltaTime; // calculate distance to move
            legLTarget.position = Vector3.MoveTowards(legLTarget.position, killTarg.position, step);
        }
    }

    public void PlayLegAudio()
    {
        legAudio.pitch = Random.Range(legPitchMin,legPitchMax);
        legAudio.Play();
    }


}
