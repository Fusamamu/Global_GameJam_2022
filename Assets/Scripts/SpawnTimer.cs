using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnTimer : MonoBehaviour
{
    [SerializeField] private bool  timerIsRunning = true;
    [SerializeField] private float spawnTimer;
    [SerializeField] private TextMeshProUGUI timerText;

    public static event Action<SpawnTimer> OnTimeToSpawn = delegate { };

    private void Update()
    {
        //if(!timerIsRunning) return;
        
        if (spawnTimer > 0)
        {
            spawnTimer -= Time.deltaTime;
        }
        else
        {
            spawnTimer = 5;
            //timerIsRunning = false;
            
            OnTimeToSpawn?.Invoke(this);
        }
           
        DisplayTime(spawnTimer);
    }
    
    private void DisplayTime(float _timeToDisplay)
    {
        float _minutes = Mathf.FloorToInt(_timeToDisplay / 60);  
        float _seconds = Mathf.FloorToInt(_timeToDisplay % 60);

        var _timeText = $"{_minutes:00}:{_seconds:00}";
        timerText.SetText(_timeText);
    }
}
