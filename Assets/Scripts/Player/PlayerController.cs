using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;

public class PlayerController : MonoBehaviour
{
    public Action OnStaminaUpdate;

    [SerializeField] private float moveCD = 0.2f;
    [SerializeField] [ReadOnly] private bool canMove;
    [SerializeField] private float inhaleForce = 7f;
    [SerializeField] private float exhaleForce = 3f;
    
    [Space]
    [FoldoutGroup("Stamina")][SerializeField] private float inhaleStamina = 2;
    [FoldoutGroup("Stamina")][SerializeField] private float exhaleStamina = 3;
    [FoldoutGroup("Stamina")][SerializeField] private float staminaRegen = 2;
    [FoldoutGroup("Stamina")][SerializeField] private int maxStamina = 15;
    
    [SerializeField] [ReadOnly] private float stamina;
    [Space]
    [FoldoutGroup("UnityEvent")] public UnityEvent OnOutStamina;
    [FoldoutGroup("UnityEvent")] public UnityEvent<Vector3> OnInhale;
    [FoldoutGroup("UnityEvent")] public UnityEvent<Vector3>  OnExhale;
    [FoldoutGroup("UnityEvent")] public UnityEvent OnCollectedGhost;
    
    private Transform mousePos;
    private Rigidbody rb;
    private Camera cam;
    private LayerMask groundMask;
    private Vector3 direction;
    private float cd = 0;

    public float Stamina => stamina;

    public int MAXStamina => maxStamina;

    // Start is called before the first frame update
    void Start()
    {
        stamina = maxStamina;
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
        groundMask = LayerMask.GetMask("Ground");
        if (!mousePos)
        {
            mousePos = new GameObject("MousePos").transform;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateInput();
        UpdateStamina();
        UpdateTime();
    }

    private void UpdateInput()
    {
        Ray _ray = cam.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray, out RaycastHit _hit, float.MaxValue, groundMask))
        {
            mousePos.transform.position = _hit.point;
        }

        var _heading = mousePos.position - transform.position;
        _heading.y = 0;
        var _distance = _heading.magnitude;
        direction = _heading / _distance;

        if (Input.GetMouseButtonUp(0)) //Exhale
        {
            Exhale();
        }
        else if (Input.GetMouseButtonUp(1)) //Inhale
        {
            Inhale();
        }
    }

    private void Exhale()
    {
        if (!canMove) return;
        
        if (stamina >= exhaleStamina)
        {
            rb.AddForce(-direction * exhaleForce, ForceMode.Impulse);
            stamina -= exhaleStamina;
            canMove = false;
            OnExhale?.Invoke(-direction);
        }
        else
        {
            OnOutStamina?.Invoke();
        }
    }

    private void Inhale()
    {
        if (!canMove) return;
        
        if (stamina >= inhaleStamina)
        {
            rb.AddForce(direction * inhaleForce, ForceMode.Impulse);
            stamina -= inhaleStamina;
            canMove = false;
            OnInhale?.Invoke(direction);
        }
        else
        {
            OnOutStamina?.Invoke();
        }
    }
    
    private void UpdateStamina()
    {
        if (stamina <= maxStamina)
        {
            stamina += staminaRegen * Time.deltaTime;
        }
        OnStaminaUpdate?.Invoke();
    }

    private void UpdateTime()
    {
        if (canMove) return;
        if (cd >= 0)
        {
            canMove = false;
            cd -= Time.deltaTime;
        }
        else
        {
            canMove = true;
            cd = moveCD;
        }
    }
}
