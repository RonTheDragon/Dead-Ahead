using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazyEnemyHealth : Health
{
    private GameManager _gameManager;
    private DamageCounterPooler _damageCounterPooler;
    [SerializeField] private Animator _animator;
    private void Start()
    {
        _gameManager = GameManager.Instance;
        _damageCounterPooler = _gameManager.DamageCounterPooler;
        _animator.SetBool("Walk", false);
    }

    public override void Spawn()
    {
        base.Spawn();
        _animator.SetBool("Death", false);
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {            
         Die();
    }
    public override void Die()
    {
        base.Die();
        _animator.SetBool("Death", true);
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
