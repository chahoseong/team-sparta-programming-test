using UnityEngine;

public abstract class EnemyState
{
    public virtual void OnEnter(EnemyController owner) { }
    public virtual void OnExit(EnemyController owner) { }
    
    public virtual void OnFixedUpdate(EnemyController owner) { }
    public virtual void OnUpdate(EnemyController owner) { }

    public virtual void OnCollisionEnter(EnemyController owner, Collision collision) { }
    public virtual void OnCollisionStay(EnemyController owner, Collision collision) { }
    public virtual void OnCollisionExit(EnemyController owner, Collision collision) { }
}
