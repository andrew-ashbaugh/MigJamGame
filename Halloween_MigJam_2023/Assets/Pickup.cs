using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    public GameObject foundPrompt;
    public bool rope;
    public bool bucket;

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
            Destroy(gameObject);
            foundPrompt.SetActive(true);
        }

    }
}
