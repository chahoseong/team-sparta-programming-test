using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D physicsBody;
    [SerializeField] private Transform trackTransform;
    
    [Header("Settings")]
    [SerializeField] private Vector2 initialVelocity;

    private bool isGrounded = true;
    
    public bool IsGrounded => isGrounded;

    private void Start()
    {
        physicsBody.velocity = initialVelocity;
    }

    private void Update()
    {
        isGrounded = transform.position.y <= trackTransform.position.y;

        if (isGrounded && physicsBody.velocity.y <= 0)
        {
            transform.position = new Vector3(transform.position.x, trackTransform.position.y, transform.position.z);
        }
        else
        {
            physicsBody.velocity += Physics2D.gravity * Time.deltaTime;
        }
    }

    public void Jump(float force)
    {
        physicsBody.AddForce(Vector2.up * force, ForceMode2D.Impulse);
    }
}
