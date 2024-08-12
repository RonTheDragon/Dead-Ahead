using UnityEngine;

public class ChasingEnemyHealth : Health
{
    [SerializeField] private LayerMask _deathLayerMask;
    private EnemyMovement _enemyMovement => GetComponent<EnemyMovement>();

    private GameManager _gameManager;
    private PlayerHealth _playerHealth;
    private DamageCounterPooler _damageCounterPooler;
    [SerializeField] private Animator _animator;

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _playerHealth = _gameManager.PlayerRefs.PlayerHealth;
        _damageCounterPooler = _gameManager.DamageCounterPooler;
    }

    public override void Spawn()
    {
        base.Spawn();
        _enemyMovement.Spawn();
        _animator.SetBool("Death", false);
    }
    public override void Die()
    {
        base.Die();
        _animator.SetBool("Death", true);
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (_deathLayerMask == (_deathLayerMask | (1 << collision.gameObject.layer)))
        {
            if (collision.gameObject.tag == "Player")
            {
                if (!_playerHealth.TryCatchPlayer(transform.position.x))
                {
                    Die();
                }
            }
            else
            {
                Die();
            }
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ramp")
        {
                Die();
        }
    }

    public override void TakeDamage(float damage)
    {
        if (!_isDead)
        {
            _damageCounterPooler.CreateOrSpawnFromPool("DamageCounter", transform.position,Quaternion.identity,_gameManager.PlayerCamera.transform).Display((int)damage);
        }
        base.TakeDamage(damage);
    }
}
