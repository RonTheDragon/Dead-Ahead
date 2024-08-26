using UnityEngine;

public class BossMovement : EnemyMovement
{
    private BossHealth _bossHealth => GetComponent<BossHealth>();
    private Camera _playerCamera;
    private PlayerHealth _playerHealth;
    [SerializeField] private float _range;

    protected new void Start()
    {
        base.Start();
        _playerCamera = _gm.PlayerCamera;
        _playerHealth = _gm.PlayerRefs.PlayerHealth;
    }
    

    protected override void Movements()
    {
        if (!_bossHealth.IsDead)
        {
            _rigidbody2D.velocity = new Vector2(_movementSpeed, 0);
            if (_playerCamera != null)
            {
                Vector3 _screenEdge = _playerCamera.ViewportToWorldPoint(new Vector3(0, 0.5f, 0));
                if (transform.position.x < _screenEdge.x) { transform.position = new Vector3(_screenEdge.x, transform.position.y, transform.position.z); }

                if (!_playerHealth.IsDead)
                {
                    BossAttacks();
                }
            }
        }
    }

    private void BossAttacks()
    {
        if (_player.position.x < transform.position.x + _range) 
        {
            _playerHealth.Die();
        }
    }
}
