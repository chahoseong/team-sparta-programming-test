using UnityEngine;

public class MoveState : EnemyState
{
    private Vector3 cachedVelocity;
    private bool isTruckContacted;
    private bool canJump = true;
    
    private Collider[] overlaps = new Collider[1];
    
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
        Vector3 point1 = owner.PhysicalBoundary.bounds.center + Vector3.up * owner.PhysicalBoundary.bounds.extents.y;
        Vector3 point2 = owner.PhysicalBoundary.bounds.center + Vector3.down * owner.PhysicalBoundary.bounds.extents.y;
        if (Physics.OverlapCapsuleNonAlloc(point1, point2,
                owner.PhysicalBoundary.bounds.extents.x,
                overlaps) <= 2)
        {
            canJump = true;
        }
        else
        {
            canJump = false;
        }
        
        cachedVelocity = owner.PhysicalBody.velocity;
    }

    public override void OnCollisionEnter(EnemyController owner, Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Vector3 deltaPosition = collision.transform.position - owner.transform.position;
            
            if (canJump && cachedVelocity.x < 0.0f && deltaPosition is { x: < 0.0f, y: >= 0.0f })
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
