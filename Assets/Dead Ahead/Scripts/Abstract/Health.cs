using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [SerializeField][ReadOnly] protected float _currentHealth;
    [SerializeField] protected float _maxHealth;
    protected bool _isDead;

    public virtual void Spawn()
    {
        _isDead=false;
        _currentHealth = _maxHealth;
    }
    public virtual void Die()
    {
        _isDead=true;
    }
    public bool IsDead()
    {
        return _isDead;
    }
}
