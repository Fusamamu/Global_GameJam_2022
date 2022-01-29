using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GhostVacuumMachine : MonoBehaviour
{
    [SerializeField] private Player mainPlayer;
    [SerializeField] private float turnSpeed = 10f;

    [SerializeField] private CapsuleCollider collider;


    private void Start()
    {
        if (mainPlayer == null)
            mainPlayer = FindObjectOfType<Player>();
        
        TurnOff();
    }

    private void Update()
    {
        transform.position = mainPlayer.transform.position;
    }

    private void OnTriggerEnter(Collider _other)
    {
        if(!_other.CompareTag("Ghost")) return;

        var _ghost = _other.GetComponent<Ghost>();
        
        _ghost.GotVacuumed();
    }

    private Vector3 GetPlayerNormalizedPlaneDirection()
    {
        var _playerController = mainPlayer.GetController();
        var _playerDir        = _playerController.GetDirection();
        
        _playerDir = new Vector3(_playerDir.x, 0, _playerDir.z);

        return _playerDir;
    }
    
    public void LookAtDirection(Vector3 _direction)
    {
        gameObject.SetActive(true);
        
        _direction.Normalize();

        if (!(_direction.sqrMagnitude > 0.001f)) return;
        
        float _toRotation  = Mathf.Atan2(_direction.z, _direction.x) * Mathf.Rad2Deg * -1;
        transform.rotation = Quaternion.Euler(0, _toRotation, 0);

        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(2);
        TurnOff();
    }

    private void TurnOn()  => gameObject.SetActive(true);
    private void TurnOff() => gameObject.SetActive(false);
}

