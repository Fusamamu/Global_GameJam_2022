using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public UnityEvent OnHitDeadZone;
    public Action OnCapturedGhost;
    public ParticleSystem captureParticle;
    [SerializeField] private PlayerController playerController;

    [Header("Audio")] [SerializeField] private AudioClip captureSfx;
    public PlayerController Controller
    {
        get => playerController;
        set => playerController = value;
    }


    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        OnHitDeadZone.AddListener(playerController.Dizzy);
        OnCapturedGhost += ShowCaptureEffect;
    }

    public void ShowCaptureEffect()
    {
        SoundManager.Instance.PlaySFX(captureSfx);
        captureParticle.Play();
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
