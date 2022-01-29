using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject creditCanvas;

    [SerializeField] private string stageSelectName = "02StageSelect";

    public void StartGame()
    {
        SceneManager.LoadScene(stageSelectName);
    }
    
    public void ShowCredits()
    {
        creditCanvas.SetActive(true);
    }

    public void HideCredits()
    {
        creditCanvas.SetActive(false);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
