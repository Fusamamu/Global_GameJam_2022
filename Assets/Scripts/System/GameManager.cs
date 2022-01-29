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
    [SerializeField] private GameplayManager gameplayManager;

    public PlayerController Player => player;

    public override void Init()
    {
        ResumeTime();
        base.Init();
        player = FindObjectOfType<PlayerController>();
        gameplayManager = FindObjectOfType<GameplayManager>();
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
        ResumeTime();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GhostManager.Instance.ClearGhost();
        //Destroy(this.gameObject);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("01Menu");
    }

    public void OnWin()
    {
        gameplayManager.Win();
    }

    public void OnGameOver()
    {
        PauseTime();
        var _mainUI = FindObjectOfType<MainUIController>();
        _mainUI.DisplayGameOverUI();
    }
}
