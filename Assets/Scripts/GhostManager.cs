using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GhostManager : Singleton<GhostManager>
{
    [SerializeField] private List<GameObject> allGhostInScene = new List<GameObject>();

    [SerializeField] private Dictionary<int, List<GameObject>> ghostsEachWave = new Dictionary<int, List<GameObject>>();

    public static event Action OnGhostRemoved = delegate {  };

    public void ClearGhost()
    {
        allGhostInScene.Clear();
        ghostsEachWave.Clear();
    }
    
    public void StoreGhost(GameObject _ghost)
    {
        allGhostInScene.Add(_ghost);
    }

    public void RemoveGhost(GameObject _ghost)
    {
        var _toBeRemoveGhost =
            allGhostInScene.FirstOrDefault(_o => _o.GetInstanceID() == _ghost.GetInstanceID());

        allGhostInScene.Remove(_toBeRemoveGhost);
        
        OnGhostRemoved?.Invoke();
    }

    public void AddGhostsEachWave(int _index, List<GameObject> _ghosts)
    {
        if (!ghostsEachWave.ContainsKey(_index))
        {
            ghostsEachWave.Add(_index, _ghosts);
        }
        else
        {
            ghostsEachWave[_index] = _ghosts;
        }
    }

    public void RemoveGhostByWaveIndex(int _index, GameObject _ghost)
    {
        if (ghostsEachWave.ContainsKey(_index))
        {
            if(ghostsEachWave[_index] == null) return;
            
            var _toBeRemoveGhost = ghostsEachWave[_index].FirstOrDefault(_o => _o.GetInstanceID() == _ghost.GetInstanceID());

            if (_toBeRemoveGhost != null)
            {
                Debug.Log("Removing Ghost ID : "  + _toBeRemoveGhost.GetInstanceID());

                ghostsEachWave[_index].Remove(_toBeRemoveGhost);
            }
        }
    }

    public int GetGhostLeftCurrentWave()
    {
        return 0;
        //return GetGhostLeftCountsByWaveIndex(c)
    }

    public int GetGhostLeftCountsByWaveIndex(int _index)
    {
        if (!ghostsEachWave.ContainsKey(_index)) return 0;
        var _ghosts = ghostsEachWave[_index];

        return _ghosts.Count;
    }
    
}

