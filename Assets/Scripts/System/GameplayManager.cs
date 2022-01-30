using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    public Action OnSwapFilter;
    public Action OnWin;
    [SerializeField] private float swapCD;
    
    [SerializeField] private MainUIController mainUIController;
    [SerializeField] private WinUI winUI;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private Canvas pauseCanvas;

    [SerializeField] private AudioClip normalBGM;
    [SerializeField] private AudioClip ghostBGM;

    private float cd;
    private bool canSwap;
    public static bool PreventSpaceBar = false;

    void Awake()
    {
        GameManager.Instance.Init();
        PreventSpaceBar = false;
        
        SoundManager.Instance.PlayPairBGM(normalBGM, ghostBGM);
        SoundManager.Instance.SwapBGM();
        
        OnWin += Win;
        CreateSceneAssets();
        Init();
    }

    private void Update()
    {
        UpdateInputListener();
        
        if (cd >= 0)
        {
            canSwap = false;
            cd -= Time.deltaTime;
        }
        else
        {
            canSwap = true;
            cd = swapCD;
        }
    }

    private void UpdateInputListener()
    {
        if(PreventSpaceBar || !canSwap) return;
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnSwapFilter?.Invoke();
            SoundManager.Instance.SwapBGM();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!GameManager.Instance.isGamePause)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }
    
    

    private void CreateSceneAssets()
    {
        var _uiControllerPref = Resources.Load<MainUIController>("Prefabs/UI/MainCanvas");
        var _winUI = Resources.Load<WinUI>("Prefabs/UI/WinCanvas");
        if (!winUI)
        {
            winUI = Instantiate(_winUI);
        }

        if (!mainUIController)
        {
            mainUIController = Instantiate(_uiControllerPref);
        }
        winUI.gameObject.SetActive(false);
        
        cameraController = gameObject.GetOrAddComponent<CameraController>();
    }

    private void Init()
    {
        mainUIController.Init();
        cameraController.Init();
        OnSwapFilter += cameraController.SwapFilter;
    }

    public void Win()
    {
        GameManager.Instance.PauseTime();
        winUI.gameObject.SetActive(true);

        var _timeLeft = FindObjectOfType<SpawnTimer>().TimeLeft;
        
        float _minutes = Mathf.FloorToInt(_timeLeft / 60);  
        float _seconds = Mathf.FloorToInt(_timeLeft % 60);

        var _timeText = $"{_minutes:00}:{_seconds:00}";
        winUI.TimeLeftText.SetText("Time Left\n" + _timeText);
    }
    
    public void ReStartGame()
    {
        GameManager.Instance.ResumeTime();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GhostManager.Instance.ClearGhost();
        //Destroy(this.gameObject);
    }

    public void PauseGame()
    {
        pauseCanvas.gameObject.SetActive(true);
        GameManager.Instance.PauseTime();
    }

    public void ResumeGame()
    {
        pauseCanvas.gameObject.SetActive(false);
        GameManager.Instance.ResumeTime();
    }

    public void ChangeScene(string _scene)
    {
        SceneManager.LoadScene(_scene);
    }
    
    public void OnGameOver()
    {
        GameManager.Instance.PauseTime();
        var _mainUI = FindObjectOfType<MainUIController>();
        _mainUI.DisplayGameOverUI();
    }
    
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("01Menu");
    }
}
