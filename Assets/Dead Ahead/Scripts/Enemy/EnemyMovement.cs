using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : Movement
{
    private EnemyHealth _enemyHealth => GetComponent<EnemyHealth>();

    private GameManager _gm;
    private Transform _player;
    
    new protected void Start()
    {
        base.Start();
        _gm = GameManager.Instance;
        _player = _gm.GetPlayer().transform;
        Movements();
    }

    private float GetPlayerDirection()
    {
        if (_player == null) return 0;
        return _player.position.y - _movingTransform.position.y;  
    }

    protected override void GotStuck()
    {
        _enemyHealth.Die();
    }

    protected override void Movements()
    {
        float dir = Mathf.Clamp(GetPlayerDirection(),-1,1);
        _rigidbody2D.velocity = new Vector2(_movementSpeed, dir * _steerSpeed);
    }

    public void Spawn()
    {
        Movements();
    }
}
