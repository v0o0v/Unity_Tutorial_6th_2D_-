using System;
using UnityEngine;

public class AmongusController : MonoBehaviour
{
    private Rigidbody rb;

    private Vector3 dir;
    
    public float moveSpeed = 5f;
    public float turnSpeed = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        dir = new Vector3(h, 0, v).normalized;
        
        Turn();
    }

    void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        // rb.linearVelocity = dir * moveSpeed;

        Vector3 targetPosition = rb.position + dir * moveSpeed;
        rb.MovePosition(targetPosition);
    }
    
    public void Turn()
    {
        if (dir.x == 0 && dir.z == 0)
            return;
        
        Quaternion newRotation = Quaternion.LookRotation(dir);

        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, turnSpeed * Time.deltaTime);
    }
}