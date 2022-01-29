using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class GhostSpawner : MonoBehaviour
{
    [SerializeField] private GameObject ghostPrefab;

    [SerializeField] private BoundsInt spawnArea;

    [SerializeField] private List<Vector3> randomSpawnPositions = new List<Vector3>();

    [SerializeField] private bool debugGizmos = true;
    
    [SerializeField] private float gizmosSphereSize = 0.05f;
    
    [SerializeField] private List<WaveData> waveDataList = new List<WaveData>();


    private void Start()
    {
        var _currentPos   = transform.position;
        
        var _posX = (int) _currentPos.x;
        var _posY = (int) _currentPos.y;
        var _posZ = (int) _currentPos.z;
        
        var _boundsCenter = new Vector3Int(_posX, _posY, _posZ);
        
        spawnArea = new BoundsInt(_boundsCenter, spawnArea.size);
        randomSpawnPositions = Utility.Random.GetRandomPointInBounds(10, spawnArea);
        
        InitialSpawnGhost();
        
        SpawnTimer.OnTimeToSpawn += OnGhostSpawned;
    }

    private void InitialSpawnGhost()
    {
        var _firstWave  = waveDataList.FirstOrDefault(_waveData => _waveData.WaveOrder == 0);
        var _spawnGhosts = SpawnGhostFromWave(_firstWave);
        
        GhostManager.Instance.AddGhostsEachWave(0, _spawnGhosts);
    }
    
    private void OnGhostSpawned(SpawnTimer _timer)
    {
        var _waveIndex   = _timer.GetCurrentWaveOrderIndex();
        var _waveData    = _timer.GetCurrentWaveData();
        var _spawnGhosts = SpawnGhostFromWave(_waveData);
        
        GhostManager.Instance.AddGhostsEachWave(_waveIndex, _spawnGhosts);
    }

    private List<GameObject> SpawnGhostFromWave(WaveData _waveData)
    {
        if (_waveData == null) return null;

        var _newGhosts = new List<GameObject>();
        
        var _spawnCount = _waveData.EnemyCount;

        for (var _i = 0; _i < _spawnCount; _i++)
        {
            Vector3 _spawnPos = Vector3.zero;

            if (_waveData.GroupSpawnPosition != null)
            {
                _spawnPos = _waveData.GroupSpawnPosition.GetChild(_i).position;
            }
        
            var _newGhost = Instantiate(ghostPrefab, _spawnPos, Quaternion.identity);

            _newGhosts.Add(_newGhost);
            GhostManager.Instance.StoreGhost(_newGhost);
        }

        return _newGhosts;
    }

    private void SpawnGhost(SpawnTimer _timer)
    {
        var _randomIndex    = Random.Range(0, randomSpawnPositions.Count - 1);
        var _randomPosition = randomSpawnPositions[_randomIndex];

        Instantiate(ghostPrefab, _randomPosition, Quaternion.identity);
    }

    public List<WaveData> GetWaveDataList()
    {
        return waveDataList;
    }

    private void Update()
    {
       
    }

    private void OnDrawGizmos()
    {
        if(!debugGizmos) return;
        
        // Gizmos.color = new Color(1, 0, 0, 0.5f);
        // Gizmos.DrawCube(transform.position, spawnArea.size);
        //
        // Gizmos.color = Color.yellow;

        if (waveDataList != null)
        {
            foreach (var _waveData in waveDataList)
            {
                var _groupSpawnPosition = _waveData.GroupSpawnPosition;

                ChangeGizmosByIndex(_waveData.WaveOrder);
                
                if(_groupSpawnPosition.childCount == 0) return;
                
                foreach (Transform _transform in _groupSpawnPosition)
                {
                    Gizmos.DrawSphere(_transform.localPosition, gizmosSphereSize);
                }
            }
        }
    }

    private void ChangeGizmosByIndex(int _index)
    {
        switch (_index)
        {
            case 0:
                Gizmos.color = Color.yellow;
                break;
            case 1:
                Gizmos.color = Color.blue;
                break;
            case 2:
                Gizmos.color = Color.red;
                break;
            case 3:
                Gizmos.color = Color.green;
                break;
            default:
                Gizmos.color = Color.yellow;
                break;
        }
    }
}


