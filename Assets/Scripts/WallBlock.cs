using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WallBlock : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private BoxCollider boxCollider;

    private void Start()
    {
        rigidbody   = gameObject.GetOrAddComponent<Rigidbody>();
        boxCollider = gameObject.GetOrAddComponent<BoxCollider>();

        rigidbody.useGravity  = false;
        rigidbody.isKinematic = false;
        rigidbody.constraints = RigidbodyConstraints.FreezeAll;

        if(GetComponent<MeshCollider>() != null)
            GetComponent<MeshCollider>().enabled = false;
    }

    private void OnCollisionEnter(Collision _other)
    {
        if(!_other.collider.CompareTag("Ghost")) return;

        var _ghost = _other.collider.GetComponent<Ghost>();

        var _ghostHitDir  = _ghost.GetCurrentDirection();
        var _hitNormal    = _other.contacts[0].normal;

        var _reflectDir   = Vector3.Reflect(_ghostHitDir, _hitNormal);

        _reflectDir = new Vector3(_reflectDir.x, 0, _reflectDir.z);
        
        _ghost.SetVelocity(_reflectDir * _ghost.Speed);
        
    }

    private Vector3 GetReflectVector(Vector3 _hit)
    {
        var _hitNormal = new Vector3(_hit.x, 0, _hit.z);

        var _reflectVec = Quaternion.AngleAxis(-20, Vector3.up) * _hitNormal;

        return _reflectVec;
    }
}
