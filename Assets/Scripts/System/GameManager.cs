using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Filter{ Normal, Ghost}

public class GameManager : Singleton<GameManager>
{
    public Filter currentFilter;
    public bool isGamePause = false;
    public bool isGameOver = false;
    
    [SerializeField] private Player player;
    [SerializeField] private GameplayManager gameplayManager;

    public Player Player => player;

    public override void Init()
    {
        ResumeTime();
        base.Init();
        isGameOver = false;
        player = FindObjectOfType<Player>();
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
    

    public void GhostCaptured()
    {
        player.OnCapturedGhost?.Invoke();
    }

    public void OnWin()
    {
        gameplayManager.Win();
    }
}
