using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour , IPlayerComponent
{
    private SOweapon _weapon;
    private int _weaponLevel;
    [SerializeField] private Transform _weaponShootFrom;
    [SerializeField] private LayerMask _attackLayerMask;
    private PlayerWeapon _weaponObject;
    private PlayerMovement _movement;
    private PlayerHealth _health;
    private bool _isShooting;
    [SerializeField] private Image _reloadImage;
    [SerializeField] private TMP_Text _ammoText;
    private GameManager _gm;
    private GameData _gameData;
    private Animator _playerAnimator;
    private Collider2D _collider;

    public void PlayerStart(PlayerRefs refs)
    {
        _gm = GameManager.Instance;
        _gameData = _gm.SaveSystem.GameData;
        _weapon = _gm.WeaponsList.Weapons[_gameData.CurrentWeaponIndex];
        _weaponLevel = _gameData.WeaponLevels[_gameData.CurrentWeaponIndex]-1;

        _movement = refs.PlayerMovement;
        _health = refs.PlayerHealth;
        _weaponObject = Instantiate(_weapon.WeaponPrefab, _weaponShootFrom.position, Quaternion.identity, transform);
        _weaponObject.SetupWeapon(this);
        _playerAnimator = refs.PlayerAnimator;
        _collider = refs.PlayerCollider;
    }

    public void SetShooting(bool shooting)
    {
        _isShooting = shooting;
    }

    public void Update()
    {
        if (_isShooting && !_movement.IsSprinting && !_health.IsDead && _collider.enabled)
        {
            ShootWeapon();
        }
    }

    private void ShootWeapon()
    {
        CancelInvoke(nameof(StopShootAnimation));
        _weaponObject.TryShoot();
        _playerAnimator.SetBool("Shoot", true);
        Invoke(nameof(StopShootAnimation), 0.5f);
    }

    private void StopShootAnimation()
    {
        _playerAnimator.SetBool("Shoot", false);
    }

    public int WeaponLevel => _weaponLevel;
    public Transform WeaponShootFrom => _weaponShootFrom;
    public LayerMask AttackLayerMask => _attackLayerMask;
    public Image ReloadImage => _reloadImage;
    public TMP_Text AmmoText => _ammoText;

}
