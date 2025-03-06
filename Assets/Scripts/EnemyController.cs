using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private EnemyMovement movement;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Transform other = GetOtherTransformFromCollision(collision);
        HandleCollision(other);
    }

    private Transform GetOtherTransformFromCollision(Collision2D collision)
    {
        if (collision.transform == transform)
        {
            return collision.otherCollider ? collision.otherCollider.transform : null;
        }
        else
        {
            return collision.collider ? collision.collider.transform : null;
        }
    }

    private void HandleCollision(Transform other)
    {
        if (movement.IsGrounded && other.CompareTag("Enemy"))
        {
            Vector2 direction = other.position - transform.position;
            if (direction.x < 0)
            {
                movement.Jump(6.0f);
            }
        }
    }
}
