using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    protected GameManager _gameManager;
    [SerializeField] protected Animator _animator;
    protected DamageCounterPooler _damageCounterPooler;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private Rigidbody2D _rb;

    // Start is called before the first frame update
    protected void Start()
    {
        _gameManager = GameManager.Instance;
        _damageCounterPooler = _gameManager.DamageCounterPooler;
    }

    public override void Die()
    {
        base.Die();
        _animator.SetBool("Death", true);
        _collider.enabled = false;
        _rb.velocity = Vector3.zero;
        Invoke(nameof(ClearEnemy), 10);
    }

    private void ClearEnemy()
    {
        gameObject.SetActive(false);
    }

    public override void Spawn()
    {
        base.Spawn();
        _animator.SetBool("Death", false);
        _collider.enabled = true;
    }

    public override void TakeDamage(float damage)
    {
        if (!_isDead)
        {
            _damageCounterPooler.CreateOrSpawnFromPool("DamageCounter", transform.position, Quaternion.identity, _gameManager.PlayerCamera.transform).Display((int)damage);
        }
        base.TakeDamage(damage);
    }
}
