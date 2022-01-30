using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveProgressBar : MonoBehaviour
{
    [SerializeField] private GhostManager ghostManager;
    [SerializeField] private GhostSpawner ghostSpawner;
    [SerializeField] private SpawnTimer spawnTimer;
    
    [SerializeField] private Slider progressBar;

    [SerializeField] private Image wave_1;
    [SerializeField] private Image wave_2;
    [SerializeField] private Image wave_3;

    [SerializeField] private Image wave_boss;

    private List<Image> allWaveImages = new List<Image>();

    private float unitValue;
    private int unitCount = 0;
    
    private void Start()
    {
        ghostManager = FindObjectOfType<GhostManager>();
        ghostSpawner = FindObjectOfType<GhostSpawner>();
        spawnTimer = FindObjectOfType<SpawnTimer>();

        unitValue = GetProgressUnitValue();
        
        GhostManager.OnGhostRemoved += OnGhostRemoveHandler;
        
        allWaveImages.Add(wave_1);
        allWaveImages.Add(wave_2);
        allWaveImages.Add(wave_3);
        allWaveImages.Add(wave_boss);
        
        allWaveImages.ForEach(_o => _o.gameObject.SetActive(false));


        SpawnTimer.OnNextWaveEntered += OnNextWaveEnteredHandler;
        
        wave_1.gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        GhostManager.OnGhostRemoved -= OnGhostRemoveHandler;
        SpawnTimer.OnNextWaveEntered -= OnNextWaveEnteredHandler;
    }

    private void OnGhostRemoveHandler()
    {
        progressBar.value += unitValue;
    }

    private void OnNextWaveEnteredHandler(int _currentWaveIndex)
    {
        // if (_currentWaveIndex == 1)
        // {
        //     wave_1.gameObject.SetActive(true);
        // }else if (_currentWaveIndex == 2)
        // {
        //     wave_2.gameObject.SetActive(true);
        // }else if (_currentWaveIndex == 3)
        // {
        //     wave_3.gameObject.SetActive(true);
        // }else if (_currentWaveIndex == 4)
        // {
        //     wave_boss.gameObject.SetActive(true);
        // }
        
        if (_currentWaveIndex == 1)
        {
            wave_2.gameObject.SetActive(true);
        }else if (_currentWaveIndex == 2)
        {
            wave_3.gameObject.SetActive(true);
        }else if (_currentWaveIndex == 3)
        {
            wave_boss.gameObject.SetActive(true);
        }
    }

    private float GetProgressUnitValue()
    {
        var _allGhostInWaveData = ghostSpawner.GetAllGhostsCountInWaveDataList();

        float _unitValue = 1.0f / _allGhostInWaveData;

        return _unitValue;
    }
}
