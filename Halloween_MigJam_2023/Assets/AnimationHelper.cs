using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationHelper : MonoBehaviour
{

    public GameObject player;
    public CinemachineVirtualCamera cam;

    public Animator fade;
    public Transform teleportTarg;

    public GameObject endingUI;

    public void EndIntroCutscene()
    {
        player.SetActive(true);
        cam.m_Follow = player.transform;

    }

    public void ApplyForce()
    {
       player.GetComponent<PlayerController>().ApplyJumpForce();
    }

    public void StopPlayerMovement()
    {
        player.GetComponent<PlayerController>().canMove = false;
    }

    public void EnablePlayerMovement()
    {
        player.GetComponent<PlayerController>().canMove = true;
    }

    public void GoDownWell()
    {
        fade.SetTrigger("FadeOut");
        player.transform.position = teleportTarg.position;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ShowEndingUI()
    {
        endingUI.SetActive(true);
    }

    public void PLAY()
    {
       SceneManager.LoadScene("Village");
    }

    public void QUIT()
    {
        Application.Quit();
    }
}
