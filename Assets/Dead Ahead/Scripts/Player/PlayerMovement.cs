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

    public void PlayerStart(PlayerRefs refs)
    {
        _rigidbody2D = refs.Rigidbody2D;
        _playerHealth = refs.PlayerHealth;
        _camera = refs.Camera;
        _movingTransform = refs.PlayerTransform;
        _camTransform = _camera.transform;
        _defaultSpeed = _movementSpeed;
        Movements();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isDead) return;
        CameraFollow();
        HandleSprints();
    }

    new protected void FixedUpdate()
    {
        StuckCheck();
        base.FixedUpdate();
    }


    protected override void Movements()
    {
        _rigidbody2D.velocity = new Vector2(_movementSpeed, GetPlayerScrollInput() * _steerSpeed);
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

    public bool IsSprinting => _isSprinting;
}
