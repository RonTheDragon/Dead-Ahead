using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _steerSpeed;
    [SerializeField] private float _cameraDistance;
    [SerializeField] private RectTransform _playerScrollInput;
    private Transform _camTransform, _playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        _camTransform = _camera.transform;
        _playerTransform = _rigidbody2D.transform;
        Movement();
    }

    // Update is called once per frame
    void Update()
    {
        CameraFollow();
    }

    private void FixedUpdate()
    {
        StuckCheck();
        Movement();
    }


    private void Movement()
    {
        _rigidbody2D.velocity = new Vector2(_movementSpeed, GetPlayerScrollInput() * _steerSpeed);
    }

    private void StuckCheck()
    {
        if (_rigidbody2D.velocity.x==0)
        {
            Destroy(gameObject);
        }
    }


    private void CameraFollow()
    {
        _camera.transform.position = new Vector3(_playerTransform.position.x + _cameraDistance, _camTransform.position.y, _camTransform.position.z);
    }

    private float GetPlayerScrollInput()
    {
        if (_playerScrollInput.localPosition.y < 1 && _playerScrollInput.localPosition.y > -1) 
            return 0;

        return _playerScrollInput.localPosition.y;
    }
}
