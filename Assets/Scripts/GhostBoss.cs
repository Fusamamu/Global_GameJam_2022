using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GhostBoss : Ghost
{
    public float CoolDowntime = 5;
    public int MaxVacuumCount = 2;
    public int CurruntVacuumCount = 0;

    public bool isShrinking = false;

    private Vector3 originScale;

    public bool temporaryUnVacuumable = false;
    public float vacuumableCoolDownTime = 2.5f;

    private void Awake()
    {
        originScale = transform.localScale;
    }

    protected override void Update()
    {
        base.Update();

        if (isShrinking)
        {
            CoolDowntime -= Time.deltaTime;

            if (CoolDowntime < 0)
            {
                CoolDowntime = 5;
                isShrinking = false;
                CurruntVacuumCount = 0;

                transform.DOScale(originScale, 1);
            }
        }

        if (temporaryUnVacuumable)
        {
            vacuumableCoolDownTime -= Time.deltaTime;

            if (vacuumableCoolDownTime < 0)
            {
                vacuumableCoolDownTime = 5;
                temporaryUnVacuumable = false;
            }
        }
    }

    public override void GotVacuumed()
    {
        // if (isGettingVacuumed) return;
        // isGettingVacuumed = true;
        if(temporaryUnVacuumable) return;

        //temporaryUnVacuumable = true;

        CurruntVacuumCount++;

        if (CurruntVacuumCount < MaxVacuumCount)
        {
            isShrinking = true;
            
            var _sequence = DOTween.Sequence();

            var _targetScale = transform.localScale / 2;
            var _duration = 0.5f;


            transform.DOScale(_targetScale, _duration);
            
            // var _mainPlayer      = FindObjectOfType<Player>();
            // var _playerPosition  = _mainPlayer.transform.position;
            //     
            // _sequence.Append(transform.DOMove(_playerPosition, _duration));
            // _sequence.Join(transform.DOScale(_targetScale, _duration).OnComplete(() =>
            // {
            //  
            // }));

           GameManager.Instance.GhostCaptured();
        }
        else
        {
            isGettingVacuumed = true;
            
            var _sequence = DOTween.Sequence();

            var _targetScale = Vector3.zero;
            var _duration = 0.5f;
                
            _sequence.Append(transform.DOPunchScale(new Vector3(0.5f, 0.5f, 0.5f), _duration, 10, 1f));
            _sequence.Join(transform.DOScale(_targetScale, _duration).OnComplete(() =>
            {
                var _spawnTimer       = FindObjectOfType<SpawnTimer>();
                var _currentWaveIndex = _spawnTimer.GetCurrentWaveOrderIndex();
                    
                GhostManager.Instance.RemoveGhostByWaveIndex(_currentWaveIndex, gameObject);
                GhostManager.Instance.RemoveGhost(gameObject);
                    
                Destroy(gameObject);
            }));

            GameManager.Instance.GhostCaptured();
        }
    }
}
