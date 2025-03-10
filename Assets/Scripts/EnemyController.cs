using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CapsuleCollider capsuleCollider;
    [SerializeField] private EnemyMovement movement;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (HasEnemyTag(collision.gameObject))
        {
            Vector3 deltaPosition = collision.transform.position - transform.position;
            if (deltaPosition is {x: < 0.0f, y: >= 0.0f })
            {
                float jumpHeight = collision.collider.bounds.size.y;
                movement.Jump(jumpHeight);
            }
        }
    }
    
    private bool HasEnemyTag(GameObject target)
    {
        return target.CompareTag("Enemy");
    }
}
