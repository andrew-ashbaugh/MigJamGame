using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    public GameObject foundPrompt;
    public bool rope;
    public bool bucket;
    public bool lamp;
    public bool dogToy;
    public bool holyWater;

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
            if (dogToy == true)
            {
                other.gameObject.GetComponent<PlayerController>().hasDogToy = true;
            }
            if (holyWater == true)
            {
                other.gameObject.GetComponent<PlayerController>().hasHolyWater = true;
            }
            Destroy(gameObject);
            foundPrompt.SetActive(true);
        }

    }
}
