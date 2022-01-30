using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RunOnStart : MonoBehaviour
{
    public UnityEvent Event;
    void Start()
    {
        Event?.Invoke();
    }
    
}
