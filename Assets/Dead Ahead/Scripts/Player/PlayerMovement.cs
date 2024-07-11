using UnityEngine;

public class PlayerMovement : Movement
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _cameraDistance;
    [SerializeField] private RectTransform _playerScrollInput;
    private Transform _camTransform;
    private PlayerHealth _playerHealth => GetComponent<PlayerHealth>();

    // Start is called before the first frame update
    new protected void Start()
    {
        base.Start();
        _camTransform = _camera.transform;
        Movements();
    }

    // Update is called once per frame
    void Update()
    {
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
    }

    private void GotStuck()
    {
        _playerHealth.Die();  
    }

    private void StuckCheck()
    {
        if (_rigidbody2D.velocity.x == 0)
        {
            GotStuck();
        }
    }

    private void CameraFollow()
    {
        _camera.transform.position = new Vector3(_movingTransform.position.x + _cameraDistance, _camTransform.position.y, _camTransform.position.z);
    }

    private float GetPlayerScrollInput()
    {
        if (_playerScrollInput.localPosition.y < 1 && _playerScrollInput.localPosition.y > -1) 
            return 0;

        return _playerScrollInput.localPosition.y;
    }
}
