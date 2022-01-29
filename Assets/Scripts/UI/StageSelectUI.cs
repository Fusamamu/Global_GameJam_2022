using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageSelectUI : MonoBehaviour
{
    [SerializeField] private List<Button> stageButtons;
    [SerializeField] private Button unlockAllButton;
    [SerializeField] private Button menuBtn;
    [SerializeField] private ProgressData progressData;

    private void Start()
    {
        UpdateProgress();
        unlockAllButton.onClick.AddListener(UnlockAll);
        menuBtn.onClick.AddListener((() => GoToStage("01Menu")));
    }

    public void UpdateProgress()
    {
        for (int _i = 0; _i < progressData.StageDatas.Count; _i++)
        {
            stageButtons[_i].interactable = progressData.StageDatas[_i].isUnlocked;
            var _index = _i;
            stageButtons[_i].onClick.AddListener(delegate { GoToStage(progressData.StageDatas[_index].name); });
        }
    }
    
    public void UnlockAll()
    {
        foreach (var _dataStage in progressData.StageDatas)
        {
            _dataStage.isUnlocked = true;
        }
        UpdateProgress();
    }

    public void GoToStage(string _stage)
    {
        SceneManager.LoadScene(_stage);
    }
}
