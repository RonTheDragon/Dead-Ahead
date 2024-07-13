using UnityEngine;

public class EnemyHealth : Health
{
    [SerializeField] private LayerMask _deathLayerMask;
    private EnemyMovement _enemyMovement => GetComponent<EnemyMovement>();

    private GameManager _gameManager;
    private PlayerHealth _playerHealth;

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _playerHealth = _gameManager.PlayerRefs.PlayerHealth;
    }

    public override void Spawn()
    {
        base.Spawn();
        _enemyMovement.Spawn();
    }
    public override void Die()
    {
        base.Die();
        gameObject.SetActive(false);
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
}
