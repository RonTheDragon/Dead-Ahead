using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour , IPlayerComponent
{
    [SerializeField] private SOweapon _weapon;
    [SerializeField] private Transform _weaponShootFrom;
    [SerializeField] private LayerMask _attackLayerMask;
    private PlayerWeapon _weaponObject;
    private PlayerMovement _movement;
    private PlayerHealth _health;
    private bool _isShooting;
    [SerializeField] private Image _reloadImage;
    [SerializeField] private TMP_Text _ammoText;

    public void PlayerStart(PlayerRefs refs)
    {
        _movement = refs.PlayerMovement;
        _health = refs.PlayerHealth;
        _weaponObject = Instantiate(_weapon.WeaponPrefab, transform.position, Quaternion.identity, transform);
        _weaponObject.SetupWeapon(_weaponShootFrom, _attackLayerMask, _reloadImage,_ammoText);
    }

    public void SetShooting(bool shooting)
    {
        _isShooting = shooting;
    }

    public void Update()
    {
        if (_isShooting && !_movement.IsSprinting && !_health.IsDead)
        {
            ShootWeapon();
        }
    }

    private void ShootWeapon()
    {
        _weaponObject.TryShoot();
    }

}
