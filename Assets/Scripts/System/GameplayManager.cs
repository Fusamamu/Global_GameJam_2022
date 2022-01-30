using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    public Action OnSwapFilter;
    public Action OnWin;
    
    [SerializeField] private MainUIController mainUIController;
    [SerializeField] private WinUI winUI;
    [SerializeField] private CameraController cameraController;

    public static bool PreventSpaceBar = false;
    
    void Awake()
    {
        GameManager.Instance.Init();
        var _winZone = FindObjectsOfType<WinZone>();
        OnWin += Win;
        CreateSceneAssets();
        Init();
    }

    private void Update()
    {
        UpdateInputListener();
    }

    private void UpdateInputListener()
    {
        if(PreventSpaceBar) return;
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnSwapFilter?.Invoke();
        }
    }
    
    

    private void CreateSceneAssets()
    {
        var _uiControllerPref = Resources.Load<MainUIController>("Prefabs/UI/MainCanvas");
        var _winUI = Resources.Load<WinUI>("Prefabs/UI/WinCanvas");
        winUI = Instantiate(_winUI);
        winUI.gameObject.SetActive(false);
        mainUIController = Instantiate(_uiControllerPref);
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
    }
    
    public void ReStartGame()
    {
        GameManager.Instance.ResumeTime();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GhostManager.Instance.ClearGhost();
        //Destroy(this.gameObject);
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
