using UnityEngine;

public class PlayerMovement : Movement
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _cameraDistance;
    [SerializeField] private RectTransform _playerScrollInput;
    private Transform _camTransform;

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
        base.FixedUpdate();
    }


    protected override void Movements()
    {
        _rigidbody2D.velocity = new Vector2(_movementSpeed, GetPlayerScrollInput() * _steerSpeed);
    }

    protected override void GotStuck()
    {
        Destroy(gameObject);
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
