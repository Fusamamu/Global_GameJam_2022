using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private float waitTime = 1f;
    private bool canChangeScene;
    [SerializeField] private AudioClip bgm;

    private void Start()
    {
        SoundManager.Instance.PlayBGM(bgm);
    }

    void Update()
    {
        if (!canChangeScene)
        {
            if (waitTime >= 0)
            {
                waitTime -= Time.deltaTime;
            }
            else
            {
                canChangeScene = true;
            }
        }
        
        if (Input.anyKey)
        {
            if (canChangeScene)
            {
                SceneManager.LoadScene("01Menu");
            }
        }
    }
}
