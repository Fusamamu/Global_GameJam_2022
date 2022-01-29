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

        rigidbody.useGravity = false;
        rigidbody.isKinematic = false;
        rigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }

    private void OnCollisionEnter(Collision _other)
    {
        if(!_other.collider.CompareTag("Ghost")) return;

        var _ghost = _other.collider.GetComponent<Ghost>();

        var _ghostHitDir  = _ghost.GetCurrentDirection();
        var _hitNormal    = _other.contacts[0].normal;
        var _reflectDir   = Vector3.Reflect(_ghostHitDir, _hitNormal);
        
        _ghost.SetVelocity(new Vector3(_reflectDir.x, 0, _reflectDir.z) * 10f);
    }
  
}
