using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float inhaleForce = 0.5f;
    [SerializeField] private float exhaleForce = 0.7f;

    private Transform mousePos;
    private Rigidbody rb;
    private Camera cam;
    private LayerMask groundMask;
    private Vector3 direction;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
        groundMask = LayerMask.GetMask("Ground");
        if (!mousePos)
        {
            mousePos = new GameObject("MousePos").transform;
        }
    }

    // Update is called once per frame
    void Update()
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
            rb.AddForce(-direction * inhaleForce, ForceMode.Impulse);
        }
        else if (Input.GetMouseButtonUp(1)) //Inhale
        {
            rb.AddForce(direction * exhaleForce, ForceMode.Impulse);
        }
    }
}
