using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StageData
{
    public string name;
    public bool isUnlocked;
    public bool isCleared;
}
[CreateAssetMenu(menuName = "Data/ProgressData")]
public class ProgressData : ScriptableObject
{
    [SerializeField] private List<StageData> stageDatas;

    public List<StageData> StageDatas
    {
        get => stageDatas;
        set => stageDatas = value;
    }
}
