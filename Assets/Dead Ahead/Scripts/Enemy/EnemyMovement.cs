using UnityEngine;

public class EnemyMovement : Movement
{
    private EnemyHealth _enemyHealth => GetComponent<EnemyHealth>();
    private Rigidbody2D _rigidbody2D => GetComponent<Rigidbody2D>();
    private GameManager _gm;
    private Transform _player;
    
    private void Start()
    {
        _movingTransform = _rigidbody2D.transform;
        _gm = GameManager.Instance;
        _player = _gm.PlayerRefs.PlayerTransform;
        Movements();
    }

    private float GetPlayerDirection()
    {
        if (_player == null) return 0;
        return _player.position.y - _movingTransform.position.y;  
    }

    protected override void Movements()
    {
        float dir = Mathf.Clamp(GetPlayerDirection(),-1,1);
        _rigidbody2D.velocity = new Vector2(_movementSpeed, dir * _steerSpeed);
        FixZ();
    }

    public void Spawn()
    {
        Movements();
    }
}
