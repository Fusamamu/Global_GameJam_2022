using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Filter{ Normal, Ghost}

public class GameManager : Singleton<GameManager>
{
    public Filter currentFilter;
    
    [SerializeField] private PlayerController player;

    public PlayerController Player => player;

    public override void Init()
    {
        base.Init();
        player = FindObjectOfType<PlayerController>();
        
    }

    public void ReStartGame()
    {
        SceneManager.LoadScene("Stage1 - Peng");
    }

    public void GoToMainMenu()
    {
        
    }

    public void OnGameOver()
    {
        var _mainUI = FindObjectOfType<MainUIController>();
        _mainUI.DisplayGameOverUI();
    }
}
