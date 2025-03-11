using System.Collections;
using UnityEngine;

public class PushingBackState : EnemyState
{
    private float pushStart;
    private float pushedDistance;
    private bool done = false;
    
    public PushingBackState(float distance)
    {
        pushedDistance = distance;
        done = false;
    }

    public override void OnEnter(EnemyController owner)
    {
        pushStart = owner.transform.position.x;
    }
    
    public override void OnFixedUpdate(EnemyController owner)
    {
        if (done)
        {
            return;
        }
        
        if (owner.PhysicalBody.velocity.x <= 0.0f)
        {
            owner.PhysicalBody.AddForce(Vector3.right * owner.MoveSpeed, ForceMode.VelocityChange);
        }

        if (owner.transform.position.x - pushStart >= pushedDistance)
        {
            done = true;
            owner.StartCoroutine(ChangeState(owner));
        }
    }

    private IEnumerator ChangeState(EnemyController owner)
    {
        yield return new WaitForSeconds(0.1f);
        owner.ChangeState(new MoveState());
    }

    public override void OnCollisionStay(EnemyController owner, Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Vector3 deltaPosition = collision.transform.position - owner.transform.position;

            if (deltaPosition.x > 0.0f && collision.rigidbody.velocity.x <= 0.0f)
            {
                EnemyController other = collision.gameObject.GetComponent<EnemyController>();
                float remainingDistance = pushedDistance - (owner.transform.position.x - pushStart);
                other.ChangeState(new PushingBackState(remainingDistance));
            }
        }
    }
}
