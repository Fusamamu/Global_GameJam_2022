using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class WallBlock : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private BoxCollider boxCollider;

    private Vector3 hitNormal;

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

        var _ghostHitDir  = _ghost.GetPlaneDirection();
        var _hitNormal    = _other.contacts[0].normal;

        _hitNormal = new Vector3(_hitNormal.x, 0, _hitNormal.z).normalized;

        hitNormal = _hitNormal;

        var _reflectDir   = Vector3.Reflect(_ghostHitDir, _hitNormal);
        
        _ghost.SetVelocity(_reflectDir * _ghost.Speed);
    }

    private Vector3 GetReflectVector(Vector3 _hit)
    {
        var _hitNormal = new Vector3(_hit.x, 0, _hit.z);

        var _reflectVec = Quaternion.AngleAxis(-20, Vector3.up) * _hitNormal;

        return _reflectVec;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (hitNormal != Vector3.zero)
        {
            var _target = hitNormal + transform.position;
            Gizmos.DrawLine(transform.position, _target * 2f);
        }
    }
}
