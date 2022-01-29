using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class GhostSpawner : MonoBehaviour
{
    [SerializeField] private GameObject ghostPrefab;

    [SerializeField] private BoundsInt spawnArea;

    [SerializeField] private List<Vector3> spawnPositions = new List<Vector3>();

    
    [SerializeField] private float gizmosSphereSize = 0.05f;

    private void Start()
    {
        var _currentPos   = transform.position;
        
        var _posX = (int) _currentPos.x;
        var _posY = (int) _currentPos.y;
        var _posZ = (int) _currentPos.z;
        
        var _boundsCenter = new Vector3Int(_posX, _posY, _posZ);
        
        spawnArea      = new BoundsInt(_boundsCenter, spawnArea.size);
        spawnPositions = Utility.Random.GetRandomPointInBounds(10, spawnArea);
        
        SpawnTimer.OnTimeToSpawn += SpawnGhost;
    }

    private void SpawnGhost(SpawnTimer _timer)
    {
        var _randomIndex    = Random.Range(0, spawnPositions.Count - 1);
        var _randomPosition = spawnPositions[_randomIndex];

        Instantiate(ghostPrefab, _randomPosition, Quaternion.identity);
    }

    private void Update()
    {
       
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.position, spawnArea.size);
        
        Gizmos.color = Color.yellow;
        if (spawnPositions != null && spawnPositions.Count > 0)
        {
            foreach (var _position in spawnPositions)
            {
                Gizmos.DrawSphere(_position, gizmosSphereSize);
            }
        }
    }
}
