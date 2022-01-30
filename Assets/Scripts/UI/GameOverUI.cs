using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Button restartBtn;
    [SerializeField] private Button backBtn;
    [SerializeField] private AudioClip gameOverAudio;

    private void Start()
    {
        var _manager = FindObjectOfType<GameplayManager>();
        restartBtn.onClick.AddListener(_manager.ReStartGame);
        backBtn.onClick.AddListener(_manager.GoToMainMenu);
    }

    public void OnGameOver()
    {
        SoundManager.Instance.PlaySFX(gameOverAudio);
    }
}
