using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    [SerializeField] private CameraController cameraController;

    private void Start()
    {
        cameraController = FindObjectOfType<CameraController>();
    }

    public void FlickerOff()
    {
        if(cameraController == null)
            cameraController = FindObjectOfType<CameraController>();
        
        StartCoroutine(Wait(0.05f, 30));
        
        cameraController.FilterOff();
       
    }
    
    public void FlickerOn()
    {
        if(cameraController == null)
            cameraController = FindObjectOfType<CameraController>();
        
        StartCoroutine(Wait(0.05f, 30));
        
        cameraController.FilterOn();
       
    }
    
    public IEnumerator FlickerLoop()
    {
        FlickerOff();
        yield return new WaitForSeconds(5);
        FlickerOn();
        yield return new WaitForSeconds(5);
    }
    
    private IEnumerator Wait(float _flipFrequent, int _flipCount)
    {
        for (var _i = 0; _i < _flipCount; _i++)
        {
            yield return new WaitForSeconds(_flipFrequent);
            cameraController.SwapFilter();
        }
    }
}
