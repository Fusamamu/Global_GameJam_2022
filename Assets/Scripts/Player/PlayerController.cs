using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField, Range(5, 10)] 
    private float speed = 5f;
    
    private Vector3 moveDirection;
    
    private Rigidbody rb;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        var _h = Input.GetAxisRaw("Horizontal");
        var _v = Input.GetAxisRaw("Vertical");
        
        moveDirection = new Vector3(_h, 0f, _v);
        moveDirection *= speed;

        rb.velocity = new Vector3(moveDirection.x, rb.velocity.y, moveDirection.z);
    }
}
