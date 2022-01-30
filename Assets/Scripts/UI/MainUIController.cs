using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUIController : MonoBehaviour
{
    [SerializeField] private PlayerController playerRef;
    [SerializeField] private Slider staminaSlider;
    [SerializeField] private Image icon;
    [SerializeField] private Sprite normal;
    [SerializeField] private Sprite tried;
    private bool isGameOver = false;
    private Canvas canvas;
    
    public PlayerController PlayerRef { get => playerRef; set => playerRef = value; }

    [SerializeField] private GameOverUI gameOverCanvas;

    public void Init()
    {
        canvas = GetComponent<Canvas>();
        //canvas.worldCamera = Camera.main;
        playerRef = GameManager.Instance.Player.Controller;
        playerRef.OnStaminaUpdate += UpdateStamina;
        staminaSlider.maxValue = playerRef.MAXStamina;

        if (gameOverCanvas == null)
        {
            gameOverCanvas = GameObject.Find("GameOverCanvas").GetComponent<GameOverUI>();
            gameOverCanvas.gameObject.SetActive(false);
        }
    }

    private void UpdateStamina()
    {
        staminaSlider.value = playerRef.Stamina;
        if (playerRef.Stamina <= playerRef.MAXStamina * 0.3)
        {
            icon.sprite = tried;
        }
        else
        {
            icon.sprite = normal;
        }
    }

    public void DisplayGameOverUI()
    {
        if (isGameOver) return;
        GameManager.Instance.isGameOver = true;
        gameOverCanvas.gameObject.SetActive(true);
        gameOverCanvas.OnGameOver();
        isGameOver = true;
    }
}
