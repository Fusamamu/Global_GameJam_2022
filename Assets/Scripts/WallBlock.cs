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
        rigidbody.isKinematic = true;
        rigidbody.constraints = RigidbodyConstraints.FreezeAll;

        if(GetComponent<MeshCollider>() != null)
            GetComponent<MeshCollider>().enabled = false;
    }

    private void OnCollisionEnter(Collision _other)
    {
        if (_other.collider.CompareTag("Ghost"))
        {
            var _ghost = _other.collider.GetComponent<Ghost>();

            var _lastVelocity = _ghost.GetNormalizedLastVelocity();
            
            var _hitNormal    = _other.contacts[0].normal;
            _hitNormal = new Vector3(_hitNormal.x, 0, _hitNormal.z).normalized;

            hitNormal = -_hitNormal;

            var _reflectDir   = Vector3.Reflect(_lastVelocity, -_hitNormal);
            
            _ghost.SetVelocity(_reflectDir * _ghost.Speed);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (hitNormal != Vector3.zero)
        {
            var _target = hitNormal * 2f + transform.position;
            Gizmos.DrawLine(transform.position, _target);
        }
    }
}
