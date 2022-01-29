using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUIController : MonoBehaviour
{
    [SerializeField] private PlayerController playerRef;
    [SerializeField] private Slider staminaSlider;
    private Canvas canvas;
    public PlayerController PlayerRef { get => playerRef; set => playerRef = value; }

    public void Init()
    {
        canvas = GetComponent<Canvas>();
        //canvas.worldCamera = Camera.main;
        playerRef = GameManager.Instance.Player;
        playerRef.OnStaminaUpdate += UpdateStamina;
        staminaSlider.maxValue = playerRef.MAXStamina;
    }

    private void UpdateStamina()
    {
        staminaSlider.value = playerRef.Stamina;
    }
}
