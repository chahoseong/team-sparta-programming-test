using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody physicalBody;
    [SerializeField] private Collider physicalBoundary;
    
    [Header("Settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpSpeed;

    private EnemyState currentState;
    private bool isGrounded;
    
    public Rigidbody PhysicalBody => physicalBody;
    public Collider PhysicalBoundary => physicalBoundary;
    
    public float MoveSpeed => moveSpeed;
    public float JumpSpeed => jumpSpeed;
    public bool IsGrounded => isGrounded;
    
    private void Start()
    {
        MoveState moveState = new MoveState();
        ChangeState(moveState);
    }
    
    private void FixedUpdate()
    {
        currentState?.OnFixedUpdate(this);
    }

    private void Update()
    {
        currentState?.OnUpdate(this);
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        currentState?.OnCollisionEnter(this, collision);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        currentState?.OnCollisionStay(this, collision);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
        currentState?.OnCollisionExit(this, collision);
    }

    public void ChangeState(EnemyState nextState)
    {
        if (currentState != nextState)
        {
            currentState?.OnExit(this);
            currentState = nextState;
            currentState?.OnEnter(this);
        }
    }
}
