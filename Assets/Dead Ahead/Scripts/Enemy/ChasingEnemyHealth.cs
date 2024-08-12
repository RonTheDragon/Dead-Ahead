using UnityEngine;

public class ChasingEnemyHealth : EnemyHealth
{
    [SerializeField] private LayerMask _deathLayerMask;
    private EnemyMovement _enemyMovement => GetComponent<EnemyMovement>();

    private PlayerHealth _playerHealth;

    protected new void Start()
    {
        base.Start();
        _playerHealth = _gameManager.PlayerRefs.PlayerHealth;
    }

    public override void Spawn()
    {
        base.Spawn();
        _enemyMovement.Spawn();
        _animator.SetBool("Walk", true);
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

}
