using System;
using System.Collections.Generic;
using UnityEngine;
using static KillInfo;

public class EnemyHealth : Health
{
    protected Action OnDeath;
    protected GameManager _gameManager;
    private Camera _playerCamera;
    [SerializeField] protected Animator _animator;
    protected UIPopUpsPooler _popupsPooler;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _damageCounterSpawnHeight;
    [SerializeField] private float _tryClearRate = 3;
    [SerializeField] private float _clearIfThatFarOutsideOfCamera = 20;
    [SerializeField] private int _moneyWorth;
    [SerializeField] private List<KillInfo> _killInfos;
    protected KillType _killType;

    private PlayerHealth _playerHealth;
    private PlayerScoreSystem _playerScoreSystem;

    // Start is called before the first frame update
    protected void Start()
    {
        _gameManager = GameManager.Instance;
        _popupsPooler = _gameManager.UIPopUpsPooler;
        _playerCamera = _gameManager.PlayerCamera;
        _playerHealth = _gameManager.PlayerRefs.PlayerHealth;
        _playerScoreSystem = _gameManager.PlayerRefs.PlayerScoreSystem;
        OnDeath += DeathReward;
    }

    public override void Die()
    {
        base.Die();
        _animator.SetBool("Death", true);
        _collider.enabled = false;
        _rb.velocity = Vector3.zero;
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
        _killType = KillType.Crash;
        _animator.SetBool("Death", false);
        _collider.enabled = true;
        InvokeRepeating(nameof(TryClearEnemy), _tryClearRate, _tryClearRate);
    }

    public override void TakeDamage(float damage)
    {
        if (!_isDead)
        {
            _killType = KillType.Killed;

            _popupsPooler.CreateOrSpawnFromPool("DamageCounter", transform.position + new Vector3(0, _damageCounterSpawnHeight,0),
                Quaternion.identity, _playerCamera.transform).Display(((int)damage).ToString());
        }
        base.TakeDamage(damage);
    }

    protected virtual void DeathReward()
    {
        if (_playerHealth.IsDead) return;

        KillInfo k = GetKillInfo();
        if (k != null)
        {
            _popupsPooler.CreateOrSpawnFromPool("KillPopUp", transform.position + new Vector3(0, _damageCounterSpawnHeight, 0),
                    Quaternion.identity, _playerCamera.transform).Display($"{k.DeathMsg}\n+ {k.Reward} $");

            _playerScoreSystem.KilledEnemy(k.Reward, k.KilledBy);
        }
    }

    private KillInfo GetKillInfo()
    {
        foreach (KillInfo k in _killInfos)
        {
            if (k.KilledBy == _killType)
            {
                return k;
            }
        }
        Debug.LogWarning("missing Kill Info");
        return null;
    }

    private void TryClearEnemy()
    {
        if (transform.position.x < _playerCamera.ScreenToWorldPoint(Vector3.zero).x - (IsDead ? 0 : _clearIfThatFarOutsideOfCamera))
        {
            ClearEnemy();
        }
    }
}
