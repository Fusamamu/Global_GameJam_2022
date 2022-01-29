using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public UnityEvent OnHitDeadZone;
    [SerializeField] private PlayerController playerController;
    
    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        OnHitDeadZone.AddListener(playerController.Dizzy);
    }

    public PlayerController GetController()
    {
        return playerController;
    }

    private void OnCollisionEnter(Collision _other)
    {
        if (_other.gameObject.CompareTag("DeadZone"))
        {
            OnHitDeadZone?.Invoke();
        }
    }
}
