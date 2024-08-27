using UnityEngine;

public class PlayerRefs : MonoBehaviour
{
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerCombat _playerCombat;
    [SerializeField] private PlayerScoreSystem _playerScoreSystem;
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _playerTransform, _playerBody, _playerShadow;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Collider2D _playerCollider;
    [SerializeField] private Animator _playerAnimator;

    private void Start()
    {
        _playerHealth.PlayerStart(this);
        _playerMovement.PlayerStart(this);
        _playerCombat.PlayerStart(this);
        _playerScoreSystem.PlayerStart(this);
    }

    public PlayerHealth PlayerHealth => _playerHealth;
    public PlayerMovement PlayerMovement => _playerMovement;
    public PlayerCombat PlayerCombat => _playerCombat;
    public PlayerScoreSystem PlayerScoreSystem => _playerScoreSystem;
    public Camera Camera => _camera;
    public Transform PlayerTransform => _playerTransform;
    public Transform PlayerBody => _playerBody;
    public Transform PlayerShadow => _playerShadow;
    public Rigidbody2D Rigidbody2D => _rigidbody2D;
    public Collider2D PlayerCollider => _playerCollider;
    public Animator PlayerAnimator => _playerAnimator;
}
