using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinUI : MonoBehaviour
{
    public void ContinueBtn(string _stage)
    {
        SceneManager.LoadScene(_stage);
    }
}
