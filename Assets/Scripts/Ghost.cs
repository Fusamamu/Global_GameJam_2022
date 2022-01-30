using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ghost : MonoBehaviour
{
    public float Speed => speed;

    [SerializeField] private GameObject vacuumParticlePrefab;
    private GameObject spawnedVacuumParticle;
    
    [SerializeField] private Rigidbody rigidbody;

    [SerializeField, Range(1, 20)] private float speed = 5;

    private Vector3 lastVelocity;

    private bool isGettingVacuumed = false;
    
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

    private void Update()
    {
        if (isGettingVacuumed)
        {
            var _mainPlayer      = FindObjectOfType<Player>();
            var _playerPosition  = _mainPlayer.transform.position;
            var _currentPosition = transform.position;
            
            transform.position = Vector3.MoveTowards(_currentPosition, _playerPosition, 5 * Time.deltaTime);
            
            if(spawnedVacuumParticle != null)
                spawnedVacuumParticle.transform.position = Vector3.MoveTowards(_currentPosition, _playerPosition, 5 * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision _other)
    {
        if (_other.collider.CompareTag("Ghost"))
        {
            var _ghost = _other.collider.gameObject;
            _ghost.GetComponent<Ghost>().ReflectBack();
            
            ReflectBack();
        }
    }

    public void ReflectBack()
    {
        var _lastVelocity = GetNormalizedLastVelocity() * -2;
        SetVelocity(_lastVelocity);
    }

    private void FixedUpdate()
    {
        lastVelocity = rigidbody.velocity;
    }

    public Vector3 GetNormalizedLastVelocity()
    {
        lastVelocity = new Vector3(lastVelocity.x, 0, lastVelocity.z).normalized;
        return lastVelocity;
    }

    public Vector3 GetPlaneNormalizedDirection()
    {
        var _currentVec = rigidbody.velocity;
        var _direction  = new Vector3(_currentVec.x, 0, _currentVec.z);
        
        return _direction.normalized;
    }
 
    public void SetVelocity(Vector3 _velocity)
    {
        rigidbody.velocity = _velocity;
    }

    public void GotVacuumed()
    {
        if (!isGettingVacuumed)
        {
            isGettingVacuumed = true;

            if(vacuumParticlePrefab != null)
                spawnedVacuumParticle = Instantiate(vacuumParticlePrefab, transform.position, Quaternion.identity);
            

            
            var _sequence = DOTween.Sequence();
            
            var _targetScale = Vector3.zero;
            var _duration = 0.5f;
            
            _sequence.Append(transform.DOPunchScale(new Vector3(0.5f, 0.5f, 0.5f), _duration, 10, 1f));
            _sequence.Join(transform.DOScale(_targetScale, _duration).OnComplete(() =>
            {
                var _spawnTimer       = FindObjectOfType<SpawnTimer>();
                var _currentWaveIndex = _spawnTimer.GetCurrentWaveOrderIndex();
                
                GhostManager.Instance.RemoveGhostByWaveIndex(_currentWaveIndex, gameObject);
                GhostManager.Instance.RemoveGhost(gameObject);
                
                Destroy(gameObject);
                
                if(spawnedVacuumParticle != null)
                    Destroy(spawnedVacuumParticle);
            }));

            GameManager.Instance.GhostCaptured();
        }
    }

    private void YoyoEffect()
    {
        var _sequence = DOTween.Sequence()
            .Append(transform.DOLocalRotate(new Vector3(0, 0, 360), 10f, RotateMode.FastBeyond360).SetRelative())
            .Join(transform.DOPunchScale(new Vector3(0.5f, 0.5f, 0.5f), 10f, 10, 1f));
        
        _sequence.SetLoops(-1, LoopType.Yoyo);
    }

    public void RandomDirection()
    {
        var _randomDirX = 1;
        var _randomDirZ = 1;

        var _normalizedDir = new Vector3(_randomDirX, 0, _randomDirZ).normalized;

        rigidbody.velocity = _normalizedDir * speed;
    }
}
