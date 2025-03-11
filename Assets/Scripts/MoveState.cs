using UnityEngine;

public class MoveState : EnemyState
{
    private Vector3 cachedVelocity;
    private bool isTruckContacted;
    
    public override void OnEnter(EnemyController owner)
    {
        owner.PhysicalBody.AddForce(Vector3.left * owner.MoveSpeed, ForceMode.VelocityChange);
    }

    public override void OnFixedUpdate(EnemyController owner)
    {
        if (owner.PhysicalBody.velocity.x > -owner.MoveSpeed)
        {
            owner.PhysicalBody.AddForce(Vector3.left * owner.MoveSpeed, ForceMode.VelocityChange);
        }
    }

    public override void OnUpdate(EnemyController owner)
    {
        cachedVelocity = owner.PhysicalBody.velocity;
    }

    public override void OnCollisionEnter(EnemyController owner, Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Vector3 deltaPosition = collision.transform.position - owner.transform.position;
            
            if (cachedVelocity.x < 0.0f && deltaPosition is { x: < 0.0f })
            {
                JumpingState jumpingState = new JumpingState(collision.collider.bounds.size.y * 0.85f);
                owner.ChangeState(jumpingState);
            }
        }
    }
    
    public override void OnCollisionStay(EnemyController owner, Collision collision)
    {
        if (collision.gameObject.CompareTag("Hero"))
        {
            isTruckContacted = true;
        }
        
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Vector3 deltaPosition = collision.transform.position - owner.transform.position;
            
            if (isTruckContacted && deltaPosition.x <= 0.0f && deltaPosition.y >= owner.PhysicalBoundary.bounds.size.y * 0.9f)
            {
                float distance = collision.collider.bounds.size.x + 0.1f;
                owner.ChangeState(new PushingBackState(distance));
            }
        }
    }

    public override void OnCollisionExit(EnemyController owner, Collision collision)
    {
        if (collision.gameObject.CompareTag("Hero"))
        {
            isTruckContacted = false;
        }
    }
}
