using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    public float FlipFrequency = 0.1f;
    public int FlipCount = 10;
    
    [SerializeField] private CameraController cameraController;

    private void Start()
    {
        cameraController = FindObjectOfType<CameraController>();
    }

    public void FlickerOff()
    {
        if(cameraController == null)
            cameraController = FindObjectOfType<CameraController>();
        
        StartCoroutine(Wait(FlipFrequency, FlipCount));
        
        cameraController.FilterOff();
       
    }
    
    public void FlickerOn()
    {
        if(cameraController == null)
            cameraController = FindObjectOfType<CameraController>();
        
        StartCoroutine(Wait(FlipFrequency, FlipCount));
        
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
