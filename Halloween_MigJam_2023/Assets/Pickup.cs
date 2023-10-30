using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    public GameObject foundPrompt;
    public bool rope;
    public bool bucket;
    public bool lamp;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (rope == true)
            {
                other.gameObject.GetComponent<PlayerController>().hasRope = true;
            }
            if (bucket == true)
            {
                other.gameObject.GetComponent<PlayerController>().hasBucket = true;
            }
            if (lamp == true)
            {
                other.gameObject.GetComponent<PlayerController>().GiveLamp();
            }
            Destroy(gameObject);
            foundPrompt.SetActive(true);
        }

    }
}
