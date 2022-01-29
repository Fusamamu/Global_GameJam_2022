using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBlock : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private BoxCollider boxCollider;

    private void Start()
    {
        rigidbody   = gameObject.GetOrAddComponent<Rigidbody>();
        boxCollider = gameObject.GetOrAddComponent<BoxCollider>();
    }
}
