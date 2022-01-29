using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class Ghost : MonoBehaviour
{
    public float Speed => speed;
    
    [SerializeField] private Rigidbody rigidbody;

    [SerializeField, Range(1, 20)] private float speed = 5;

    private void Start()
    {
        if(gameObject.TryGetComponent<Rigidbody>(out var _rigidbody))
            rigidbody = _rigidbody;
        else
            rigidbody = gameObject.AddComponent<Rigidbody>();

        rigidbody.useGravity = false;
        rigidbody.constraints = RigidbodyConstraints.FreezePositionY;
        rigidbody.constraints = RigidbodyConstraints.FreezeRotation;

        RandomDirection();
    }

    public Vector3 GetCurrentDirection()
    {
        return rigidbody.velocity.normalized;
    }
 
    public void SetVelocity(Vector3 _velocity)
    {
        rigidbody.velocity = _velocity;
    }

    public void KeepMoving()
    {
        if (rigidbody.velocity.magnitude < 1)
        {
           RandomDirection();
        }
    }

    public void RandomDirection()
    {
        var _randomDirX = Random.Range(0, 1f);
        var _randomDirZ = Random.Range(0, 1f);

        var _normalizedDir = new Vector3(_randomDirX, 0, _randomDirZ).normalized;

        rigidbody.velocity = _normalizedDir * speed;
    }
}
