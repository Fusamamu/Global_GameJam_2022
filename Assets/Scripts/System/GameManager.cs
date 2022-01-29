using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Filter{ Normal, Ghost}

public class GameManager : Singleton<GameManager>
{
    public Filter currentFilter;
    public bool isGamePause = false;
    
    [SerializeField] private PlayerController player;

    public PlayerController Player => player;

    public override void Init()
    {
        ResumeTime();
        base.Init();
        player = FindObjectOfType<PlayerController>();
        
    }

    public void PauseTime()
    {
        Time.timeScale = 0;
        isGamePause = true;
    }

    public void ResumeTime()
    {
        Time.timeScale = 1;
        isGamePause = false;
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
