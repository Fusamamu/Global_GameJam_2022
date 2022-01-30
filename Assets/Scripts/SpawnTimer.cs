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

    public float WaitForFlipTimeInitial_1 = 3f;
    public float WaitForFlipTimeInitial_2 = 3f;
    
    public float WaitForFlipTimeLoop = 5f;

    public bool HavingBoss = true;
    
    public static event Action<int> OnNextWaveEntered = delegate {  };
    
    public enum WaveMode
    {
        Normal,  Boss
    }

    private WaveMode mode = WaveMode.Normal;

    private bool InitialStartBossMode = true;
    private bool StartLoopFlicker     = false;
    private bool stillFightBoss = false;

    public float TimeLeft = 0;
    
    private void Start()
    {
        waveDataList    = FindObjectOfType<GhostSpawner>().GetWaveDataList();
        
        gameplayManager = FindObjectOfType<GameplayManager>();
    }

    private void Update()
    {
        switch (mode)
        {
            case WaveMode.Normal:
                UpdateNormalMode();
                break;
            case WaveMode.Boss:
                UpdateBossMode();
                break;
        }
    }

    private void UpdateNormalMode()
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
                var _nextWaveIndex = currentWaveOrder + 1;
                var _nextWave      = GetWaveDataByOrderIndex(_nextWaveIndex);
                _nextWave.TimeLimit += _currentWave.TimeLimit;

                currentWaveOrder++;
                OnTimeToSpawn?.Invoke(this);
                OnNextWaveEntered?.Invoke(currentWaveOrder);

                if (HavingBoss)
                {
                    if (currentWaveOrder == waveDataList.Count - 1)
                    {
                        mode = WaveMode.Boss;
                        GameplayManager.PreventSpaceBar = true;
                    }
                }
            }
        }
        else
        {
            var _ghostLeftCount = GhostManager.Instance.GetGhostLeftCountsByWaveIndex(currentWaveOrder);
            if (_ghostLeftCount > 0)
            {
                gameplayManager.OnGameOver();
                DisplayTime(0);
                timerText.gameObject.SetActive(false);
                return;
            }
            
            _currentWave.TimeLimit = 0;
            
            currentWaveOrder++;
            OnTimeToSpawn?.Invoke(this);
            OnNextWaveEntered?.Invoke(currentWaveOrder);

            if (HavingBoss)
            {
                if (currentWaveOrder == waveDataList.Count - 1)
                {
                    mode = WaveMode.Boss;
                    GameplayManager.PreventSpaceBar = true;
                }
            }
        }

        TimeLeft = _currentWave.TimeLimit;
        DisplayTime(_currentWave.TimeLimit);
    }
    
    private void UpdateBossMode()
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
        
        
        if (InitialStartBossMode)
        {
            StartCoroutine(StartBossSequence());
            InitialStartBossMode = false;
        }

        var _lastWaveIndex = waveDataList.Count - 1;
        var _lastWave      = GetWaveDataByOrderIndex(_lastWaveIndex);
        
        if (_lastWave.TimeLimit > 0)
        {
            _lastWave.TimeLimit -= Time.deltaTime;

            stillFightBoss = true;

            if (StartLoopFlicker)
                StartCoroutine(LoopFlicker());
            
            var _ghostLeftCount = GhostManager.Instance.GetGhostLeftCountsByWaveIndex(_lastWaveIndex);
            if (_ghostLeftCount == 0)
            {
                currentWaveOrder++;
            }
        }
        else
        {
            stillFightBoss = false;
            StopCoroutine(LoopFlicker());
            StageManager.Instance.FlickerOn();
            
            var _ghostLeftCount = GhostManager.Instance.GetGhostLeftCountsByWaveIndex(_lastWaveIndex);
            if (_ghostLeftCount > 0)
            {
                gameplayManager.OnGameOver();
                Debug.Log("Game Over");
                DisplayTime(0);
                return;
            }
        }

        TimeLeft = _lastWave.TimeLimit;
        DisplayTime(_lastWave.TimeLimit);
    }

    private IEnumerator StartBossSequence()
    {
        StageManager.Instance.FlickerOn();
        
        yield return new WaitForSeconds(WaitForFlipTimeInitial_1);
        StageManager.Instance.FlickerOff();
        
        yield return new WaitForSeconds(WaitForFlipTimeInitial_2);
        StageManager.Instance.FlickerOn();

        StartLoopFlicker = true;
    }

    private IEnumerator LoopFlicker()
    {
        StartLoopFlicker = false;
        
        while (stillFightBoss)
        {
            yield return new WaitForSeconds(WaitForFlipTimeLoop);
            StageManager.Instance.FlickerOff();
            
            yield return new WaitForSeconds(WaitForFlipTimeLoop);
            StageManager.Instance.FlickerOn();
        }
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