using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class SpawnTimer : MonoBehaviour
{
    [SerializeField] private bool  timerIsRunning = true;
    [SerializeField] private float spawnTimer;
    [SerializeField] private TextMeshProUGUI timerText;

    public static event Action<SpawnTimer> OnTimeToSpawn = delegate { };

    [SerializeField] private int currentWaveOrder = 0;
   
    private List<WaveData> waveDataList = new List<WaveData>();

    private void Start()
    {
        waveDataList = FindObjectOfType<GhostSpawner>().GetWaveDataList();
    }

    private void Update()
    {
        if (currentWaveOrder > waveDataList.Count - 1) return;
        
        var _currentWave = GetWaveDataByOrderIndex(currentWaveOrder);

        if (_currentWave.TimeLimit > 0)
        {
            _currentWave.TimeLimit -= Time.deltaTime;
        }
        else
        {
            currentWaveOrder++;
            OnTimeToSpawn?.Invoke(this);
        }
           
        DisplayTime(_currentWave.TimeLimit);
    }

    public int GetCurrentWaveOrderIndex()
    {
        return currentWaveOrder;
    }

    public WaveData GetCurrentWaveData()
    {
        return GetWaveDataByOrderIndex(currentWaveOrder);
    }

    private WaveData GetWaveDataByOrderIndex(int _orderIndex)
    {
        return waveDataList.FirstOrDefault(_data => _data.WaveOrder == _orderIndex);
    }
    
    private void DisplayTime(float _timeToDisplay)
    {
        float _minutes = Mathf.FloorToInt(_timeToDisplay / 60);  
        float _seconds = Mathf.FloorToInt(_timeToDisplay % 60);

        var _timeText = $"{_minutes:00}:{_seconds:00}";
        timerText.SetText(_timeText);
    }
}

[System.Serializable]
public class WaveData
{
    public int   WaveOrder;
    public int   EnemyCount;
    public float TimeLimit;
    public Transform GroupSpawnPosition;
    //public List<Vector3> SpawnPositions = new List<Vector3>();
}