using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeColl : MonoBehaviour
{
    public GameObject prompt;
    float timer;
    bool showingPrompt;

    private void FixedUpdate()
    {
        if (showingPrompt == true)
        {
            timer += Time.fixedDeltaTime;

            if (timer >= 2)
            {
                prompt.SetActive(false);
                showingPrompt = false;
                timer =0;
            }
        }

    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            prompt.SetActive(true);
            showingPrompt = true;
        }
    }
}
