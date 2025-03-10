using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody characterBody;
    [SerializeField] private CapsuleCollider capsuleCollider; 

    [Header("Settings")]
    [SerializeField] private float moveVelocity;
    [SerializeField] private float jumpSpeed;

    private WaitForFixedUpdate waitForFixedUpdate = new();

    private void FixedUpdate()
    {
        if (characterBody.velocity.y <= 0.0f)
        {
            if (Mathf.Abs(characterBody.velocity.x) < Mathf.Abs(moveVelocity))
            {
                characterBody.AddForce(Vector3.right * moveVelocity, ForceMode.VelocityChange);
            }
        }
    }
    
    public void Jump(float height)
    {
        StartCoroutine(Jumping(height));
    }

    private IEnumerator Jumping(float height)
    {
        yield return waitForFixedUpdate;

        characterBody.AddForce(Vector3.up * jumpSpeed, ForceMode.VelocityChange);
        characterBody.useGravity = false;
        
        float destination = transform.position.y + height;
        yield return new WaitWhile(() => transform.position.y < destination);

        characterBody.AddForce(Vector3.right * moveVelocity, ForceMode.VelocityChange);
        characterBody.useGravity = true;
    }
}
