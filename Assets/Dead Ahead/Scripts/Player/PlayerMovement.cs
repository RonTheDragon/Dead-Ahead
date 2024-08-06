using System;
using UnityEngine;

public class PlayerMovement : Movement , IPlayerComponent
{
    [SerializeField] private float _cameraDistance;
    [SerializeField] private RectTransform _playerScrollInput;
    private Transform _camTransform;
    private PlayerHealth _playerHealth;
    private Rigidbody2D _rigidbody2D;
    private Camera _camera;
    private float _defaultSpeed;
    [SerializeField] private float _sprintBoost, _sprintLose, _cameraSprint;
    private bool _isSprinting, _moved, _isDead;
    private Transform _playerBody,_playerShadow;
    [SerializeField] private float _jumpTime, _jumpUpSpeed,_gravity;
    private float _currentJumpTime;
    [SerializeField] private float _maxY, _minY;
    private Collider2D _collider;

    public void PlayerStart(PlayerRefs refs)
    {
        _rigidbody2D = refs.Rigidbody2D;
        _playerHealth = refs.PlayerHealth;
        _camera = refs.Camera;
        _movingTransform = refs.PlayerTransform;
        _playerBody = refs.PlayerBody;
        _playerShadow = refs.PlayerShadow;
        _camTransform = _camera.transform;
        _defaultSpeed = _movementSpeed;
        _collider = refs.PlayerCollider;
        Movements();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isDead) return;
        HandleSprints();
        HandleJump();
        HandleGravity();
    }

    private void LateUpdate()
    {
        if (_isDead) return;
        CameraFollow();
    }

    new protected void FixedUpdate()
    {
        StuckCheck();
        base.FixedUpdate();
    }


    protected override void Movements()
    {
        _rigidbody2D.velocity = new Vector2(_movementSpeed, GetPlayerScrollInput() * _steerSpeed);

        if (_movingTransform.position.y < _minY || _movingTransform.position.y > _maxY)
        {
            transform.position = new Vector3(
                transform.position.x,
                Mathf.Clamp(_movingTransform.position.y, _minY, _maxY),
                transform.position.z
            );
        }

    }

    private void HandleSprints()
    {
        if (_isSprinting)
        {
            _movementSpeed += _sprintBoost * Time.deltaTime;
        }
        else
        {
            _movementSpeed -= _sprintLose * Time.deltaTime;
            if (_movementSpeed< _defaultSpeed)
            {
                _movementSpeed = _defaultSpeed;
            }
        }
    }

    private void GotStuck()
    {
        _playerHealth.Die();  
    }

    private void StuckCheck()
    {
        if (_rigidbody2D.velocity.x == 0 )
        {
            GotStuck();
        }
    }

    private void CameraFollow()
    {
        _camTransform.position = new Vector3(_movingTransform.position.x + (_cameraDistance - ((_movementSpeed-_defaultSpeed)*_cameraSprint)), _camTransform.position.y, _camTransform.position.z);
    }

    private float GetPlayerScrollInput()
    {
        if (_playerScrollInput.localPosition.y < 1 && _playerScrollInput.localPosition.y > -1) 
            return 0;

        return _playerScrollInput.localPosition.y;
    }

    public void SetSprint(bool Sprinting)
    {
        _isSprinting= Sprinting;
    }

    public void Die()
    {
        _movementSpeed = 0;
        _steerSpeed = 0;
        _isDead = true;
    }

    public void Jump()
    {
        _currentJumpTime = _jumpTime;
        _collider.enabled = false;
    }

    private void HandleJump()
    {
        if (_currentJumpTime > 0)
        {
            _currentJumpTime -= Time.deltaTime;
            _playerBody.localPosition += _jumpUpSpeed * Time.deltaTime * Vector3.up;
        }
        else if (_currentJumpTime < 0)
        {
            _currentJumpTime = 0;
        }
    }

    private void HandleGravity()
    {
        if (_playerBody.localPosition.y > 0)
        {
            Collider2D colliderAtPosition = Physics2D.OverlapPoint(transform.position);
            if (colliderAtPosition != null)
            {
                if (colliderAtPosition.transform.tag == "HighGround")
                {
                    _playerShadow.localPosition = new Vector3(_playerShadow.localPosition.x, 0.55f, _playerShadow.localPosition.z);
                }
            }
            else
            {
                _playerShadow.localPosition = new Vector3(_playerShadow.localPosition.x, 0, _playerShadow.localPosition.z);
            }
        }

        if (_currentJumpTime == 0)
        {
            if (_playerBody.localPosition.y <= 0) { _collider.enabled = true;  return; }

            if (_playerBody.localPosition.y > _playerShadow.localPosition.y)
            {
                _playerBody.localPosition -= _gravity * Time.deltaTime * Vector3.up;
            }
            else if (_playerBody.localPosition.y < _playerShadow.localPosition.y)
            {
                _playerBody.localPosition = new Vector3(_playerBody.localPosition.x, _playerShadow.localPosition.y, _playerBody.localPosition.z);
            }
        }
    }

    public bool IsSprinting => _isSprinting;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ramp")
        {
            Jump();
        }     
    }
}
