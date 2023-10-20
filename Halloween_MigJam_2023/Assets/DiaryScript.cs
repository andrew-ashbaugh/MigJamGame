using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiaryScript : MonoBehaviour
{
    [Header("Diary Info")]
    public string diaryText;
    public int diaryNum;
    public GameObject diaryVisual;
    public TextMeshProUGUI diaryTextMeshPro;
    public TextMeshProUGUI diaryNumberTextMeshPro;

    [Header("Settings")]
    public bool canInteract;
    public Vector3 offset;
    public float radius;
    public LayerMask playerLayer;
    public GameObject ePrompt;

    // Update is called once per frame
    void FixedUpdate()
    {
        Collider2D[] colls = Physics2D.OverlapCircleAll(transform.position + offset, radius, playerLayer);

        if (colls.Length > 0)
        {
            canInteract = true;
            ePrompt.SetActive(true);
        }
        else
        {
            canInteract = false;
            ePrompt.SetActive(false);
        }
    }

    void Update()
    {
        if (canInteract == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                diaryTextMeshPro.text = diaryText;
                diaryNumberTextMeshPro.text = "Diary " + diaryNum.ToString(); 
                diaryVisual.SetActive(true);
                diaryVisual.GetComponent<Animator>().SetBool("IsClosed",false);
            }

        }

    }
    void OnDrawGizmosSelected()
    {
        // Draw a semitransparent red cube at the transforms position
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawWireSphere(transform.position + offset, radius);
    }
}
