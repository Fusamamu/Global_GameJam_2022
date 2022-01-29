using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
}
