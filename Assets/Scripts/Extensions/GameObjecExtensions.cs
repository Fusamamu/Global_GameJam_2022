using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjecExtensions 
{

    public static T GetOrAddComponent<T>(this GameObject _gameObject) where T : Component
    {
        if (_gameObject.TryGetComponent<T>(out var _component))
            return _component;
            
        return _gameObject.AddComponent<T>();
    }
}
