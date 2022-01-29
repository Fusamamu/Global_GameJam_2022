using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float inhaleForce = 7f;
    [SerializeField] private float exhaleForce = 3f;
    [Space]
    [FoldoutGroup("Stamina")][SerializeField] private float inhaleStamina = 2;
    [FoldoutGroup("Stamina")][SerializeField] private float exhaleStamina = 3;
    [FoldoutGroup("Stamina")][SerializeField] private float staminaRegen = 2;
    [FoldoutGroup("Stamina")][SerializeField] private int maxStamina = 15;
    [SerializeField] [ReadOnly] private float stamina;
    
    private Transform mousePos;
    private Rigidbody rb;
    private Camera cam;
    private LayerMask groundMask;
    private Vector3 direction;
    
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
            if (stamina >= exhaleForce)
            {
                rb.AddForce(-direction * exhaleForce, ForceMode.Impulse);
                stamina -= exhaleForce;
            }
        }
        else if (Input.GetMouseButtonUp(1)) //Inhale
        {
            if (stamina >= inhaleForce)
            {
                rb.AddForce(direction * inhaleForce, ForceMode.Impulse);
                stamina -= inhaleForce;
            }
        }
    }

    private void UpdateStamina()
    {
        if (stamina <= maxStamina)
        {
            stamina += staminaRegen * Time.deltaTime;
        }
    }
}
