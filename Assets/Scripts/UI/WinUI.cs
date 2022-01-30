using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinUI : MonoBehaviour
{
    public TextMeshProUGUI TimeLeftText;
    
    public void ContinueBtn(string _stage)
    {
        SceneManager.LoadScene(_stage);
    }
}
