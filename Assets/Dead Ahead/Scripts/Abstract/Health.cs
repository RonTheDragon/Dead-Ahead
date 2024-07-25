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

    public bool IsDead => _isDead;

    public void TakeDamage(float damage)
    {
        if (!_isDead)
        {
            _currentHealth -= damage;
            if (_currentHealth <= 0)
            {
                Die();
            }
        }
    }
}
