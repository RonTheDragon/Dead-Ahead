using UnityEngine;

public class PlayerCombat : MonoBehaviour , IPlayerComponent
{
    [SerializeField] private SOweapon _weapon;
    [SerializeField] private Transform _weaponShootFrom;
    [SerializeField] private LayerMask _attackLayerMask;
    private PlayerWeapon _weaponObject;
    private bool _isShooting;

    public void PlayerStart(PlayerRefs refs)
    {
        _weaponObject = Instantiate(_weapon.WeaponPrefab, transform.position, Quaternion.identity, transform);
        _weaponObject.SetupWeapon(_weaponShootFrom, _attackLayerMask);
    }

    public void ShootPress()
    {
        _isShooting = true;
    }

    public void ShootRelease()
    {
        _isShooting = false;
    }

    public void Update()
    {
        if (_isShooting)
        {
            ShootWeapon();
        }
    }

    private void ShootWeapon()
    {
        _weaponObject.TryShoot();
    }

}
