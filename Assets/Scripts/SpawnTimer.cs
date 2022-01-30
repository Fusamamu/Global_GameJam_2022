using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class SpawnTimer : MonoBehaviour
{
    [SerializeField] private bool  timerIsRunning = true;
    [SerializeField] private float spawnTimer;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField][ReadOnly]private bool isClear = false;
    public static event Action<SpawnTimer> OnTimeToSpawn = delegate { };

    [SerializeField] private int currentWaveOrder = 0;
   
    private List<WaveData> waveDataList = new List<WaveData>();
    private GameplayManager gameplayManager;

    private void Start()
    {
        waveDataList = FindObjectOfType<GhostSpawner>().GetWaveDataList();
        gameplayManager = FindObjectOfType<GameplayManager>();
    }

    private void Update()
    {
        if (currentWaveOrder > waveDataList.Count - 1)
        {
            if (!isClear)
            {
                GameManager.Instance.OnWin();
                isClear = true;
                return;
            }
            return;
        }
        
        var _currentWave = GetWaveDataByOrderIndex(currentWaveOrder);

        if (_currentWave.TimeLimit > 0)
        {
            _currentWave.TimeLimit -= Time.deltaTime;
            
            var _ghostLeftCount = GhostManager.Instance.GetGhostLeftCountsByWaveIndex(currentWaveOrder);
            if (_ghostLeftCount == 0)
            {
                gameplayManager.OnGameOver();
                Debug.Log("Game Over");
                DisplayTime(0);
                return;
            }
        }
        else
        {
            currentWaveOrder++;
            OnTimeToSpawn?.Invoke(this);

            if (currentWaveOrder == waveDataList.Count - 1)
            {
                StartCoroutine(StartBossSequence());
            }
        }
           
        DisplayTime(_currentWave.TimeLimit);
    }


    private IEnumerator StartBossSequence()
    {
        yield return new WaitForSeconds(2);
        StageManager.Instance.FlickerOff();
        
        yield return new WaitForSeconds(5);
        StageManager.Instance.FlickerOn();
        
        //yield return new WaitForSeconds(10);
        
        // var _lastWave = GetWaveDataByOrderIndex(currentWaveOrder);
        // while (_lastWave.TimeLimit > 0)
        // {
        //     StartCoroutine(StageManager.Instance.FlickerLoop());
        // }
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
    
    public List<GameObject> GhostPrefab;
}