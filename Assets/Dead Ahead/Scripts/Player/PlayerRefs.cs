using UnityEngine;

public class PlayerRefs : MonoBehaviour
{
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _playerHealth.PlayerStart(this);
        _playerMovement.PlayerStart(this);
    }

    public PlayerHealth PlayerHealth => _playerHealth;
    public PlayerMovement PlayerMovement => _playerMovement;
    public Camera Camera => _camera;
    public Transform PlayerTransform => _playerTransform;
    public Rigidbody2D Rigidbody2D => _rigidbody2D;
}
