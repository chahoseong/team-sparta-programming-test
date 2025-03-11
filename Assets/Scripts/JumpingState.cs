using UnityEngine;

public class JumpingState : EnemyState
{
    private float jumpStart;
    private float jumpHeight;

    public JumpingState(float height)
    {
        jumpHeight = height;
    }

    public override void OnEnter(EnemyController owner)
    {
        jumpStart = owner.transform.position.y;
        owner.PhysicalBody.AddForce(Vector3.up * owner.JumpSpeed, ForceMode.VelocityChange);
    }
    
    public override void OnFixedUpdate(EnemyController owner)
    {
        if (owner.transform.position.y - jumpStart < jumpHeight && owner.PhysicalBody.velocity.y >= 0.0f)
        {
            owner.PhysicalBody.AddForce(-Physics.gravity, ForceMode.Acceleration);
        }
        else
        {
            owner.PhysicalBody.AddForce(Vector3.left * (owner.MoveSpeed + 1.0f), ForceMode.VelocityChange);
            owner.ChangeState(new FallingState());
        }
    }

    public override void OnCollisionEnter(EnemyController owner, Collision collision)
    {
        if (owner.PhysicalBody.velocity.y > owner.JumpSpeed)
        {
            owner.PhysicalBody.velocity = new Vector3(owner.PhysicalBody.velocity.x, owner.JumpSpeed, owner.PhysicalBody.velocity.z);
        }
        owner.ChangeState(new FallingState());
    }
}
