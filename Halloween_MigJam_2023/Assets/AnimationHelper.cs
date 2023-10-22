using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class AnimationHelper : MonoBehaviour
{

    public GameObject player;
    public CinemachineVirtualCamera cam;

    public void EndIntroCutscene()
    {
        player.SetActive(true);
        cam.m_Follow = player.transform;

    }
}
