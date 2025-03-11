using UnityEngine;

public class FallingState : EnemyState
{
    public override void OnCollisionEnter(EnemyController owner, Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            OnLanded(owner);
        }
        
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyController other = collision.gameObject.GetComponent<EnemyController>();
            if (other.IsGrounded)
            {
                OnLanded(owner);
            }
        }
    }

    private void OnLanded(EnemyController owner)
    {
        owner.ChangeState(new MoveState());
    }
}
