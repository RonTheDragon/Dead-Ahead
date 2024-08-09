using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazyEnemyHealth : Health
{
    private GameManager _gameManager;
    private DamageCounterPooler _damageCounterPooler;
    private void Start()
    {
        _gameManager = GameManager.Instance;
        _damageCounterPooler = _gameManager.DamageCounterPooler;
    }
    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {            
         Die();
    }
    public override void Die()
    {
        base.Die();
        gameObject.SetActive(false);
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
