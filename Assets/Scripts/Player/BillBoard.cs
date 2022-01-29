using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    [ReadOnly] [SerializeField] private Camera camera;

    private void Start()
    {
        camera = Camera.main;
        if (camera != null)
        {
            var _child = transform.GetChild(0);
            var _transform = _child.transform;
            
            _transform.rotation = camera.transform.rotation;
            
            var _currentAngle = _transform.eulerAngles;
            _transform.eulerAngles = new Vector3(_currentAngle.x, _currentAngle.y + 180, _currentAngle.z);
        }
    }
}
