using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinZone : MonoBehaviour
{
    public Action OnWin;

    private GameplayManager gameplayManager;
    private void Start()
    {
        gameplayManager = FindObjectOfType<GameplayManager>();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameplayManager.OnWin?.Invoke();
        }
    }
}
