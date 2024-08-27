using System;
using UnityEngine;

public class EnemyHealth : Health
{
    protected Action OnDeath;
    protected GameManager _gameManager;
    private Camera _playerCamera;
    [SerializeField] protected Animator _animator;
    protected DamageCounterPooler _damageCounterPooler;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _damageCounterSpawnHeight;
    [SerializeField] private float _tryClearRate = 3;
    [SerializeField] private float _clearIfThatFarOutsideOfCamera = 20;
    [SerializeField] private int _moneyWorth;

    // Start is called before the first frame update
    protected void Start()
    {
        _gameManager = GameManager.Instance;
        _damageCounterPooler = _gameManager.DamageCounterPooler;
        _playerCamera = _gameManager.PlayerCamera;
    }

    public override void Die()
    {
        base.Die();
        _animator.SetBool("Death", true);
        _collider.enabled = false;
        _rb.velocity = Vector3.zero;
        _gameManager.PlayerRefs.PlayerScoreSystem.KilledEnemy(_moneyWorth);
        OnDeath?.Invoke();
    }

    private void ClearEnemy()
    {
        gameObject.SetActive(false);
        CancelInvoke(nameof(TryClearEnemy));
    }

    public override void Spawn()
    {
        base.Spawn();
        _animator.SetBool("Death", false);
        _collider.enabled = true;
        InvokeRepeating(nameof(TryClearEnemy), _tryClearRate, _tryClearRate);
    }

    public override void TakeDamage(float damage)
    {
        if (!_isDead)
        {
            _damageCounterPooler.CreateOrSpawnFromPool("DamageCounter", transform.position + new Vector3(0, _damageCounterSpawnHeight,0),
                Quaternion.identity, _playerCamera.transform).Display((int)damage);
        }
        base.TakeDamage(damage);
    }

    private void TryClearEnemy()
    {
        if (transform.position.x < _playerCamera.ScreenToWorldPoint(Vector3.zero).x - (IsDead ? 0 : _clearIfThatFarOutsideOfCamera))
        {
            ClearEnemy();
        }
    }
}
