using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public Action OnSwapFilter;
    [SerializeField] private MainUIController mainUIController;
    [SerializeField] private CameraController cameraController;
    void Awake()
    {
        GameManager.Instance.Init();
        CreateSceneAssets();
        Init();
    }

    private void Update()
    {
        UpdateInputListener();
    }

    private void UpdateInputListener()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnSwapFilter?.Invoke();
        }
    }

    private void CreateSceneAssets()
    {
        var _uiControllerPref = Resources.Load<MainUIController>("Prefabs/MainCanvas");
        mainUIController = Instantiate(_uiControllerPref);
        cameraController = gameObject.GetOrAddComponent<CameraController>();
    }

    private void Init()
    {
        mainUIController.Init();
        cameraController.Init();
        OnSwapFilter += cameraController.SwapFilter;
    }
}
