using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;

public class Movimiento : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float turnSpeed = 50f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float moveDirection = 0f;
        if (Input.GetKey(KeyCode.W))
            moveDirection = 1f;
        if (Input.GetKey(KeyCode.S))
            moveDirection = -1f;

        float turnDirection = 0f;
        if (Input.GetKey(KeyCode.A))
            turnDirection = -1f;
        if (Input.GetKey(KeyCode.D))
            turnDirection = 1f;

        // Mover hacia adelante/atrás
        Vector3 move = transform.forward * moveDirection * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + move);

        // Rotar
        Quaternion turn = Quaternion.Euler(0f, turnDirection * turnSpeed * Time.fixedDeltaTime, 0f);
        rb.MoveRotation(rb.rotation * turn);
    }
}
