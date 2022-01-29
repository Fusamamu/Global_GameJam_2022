using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Button restartBtn;
    [SerializeField] private Button backBtn;

    private void Start()
    {
        restartBtn.onClick.AddListener(GameManager.Instance.ReStartGame);
        backBtn.onClick.AddListener(GameManager.Instance.GoToMainMenu);
    }
}
