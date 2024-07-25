using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private SOweapon _weaponData;
    private int _ammo;
    private bool _isShotCooldown;
    private Transform _shootingFrom;
    private LayerMask _layerMask;

    public void SetupWeapon(Transform shootingFrom,LayerMask attackLayerMask)
    {
        _ammo = _weaponData.MaxAmmo;
        _isShotCooldown = false;
        _shootingFrom = shootingFrom;
        _layerMask = attackLayerMask;
    }

    public void TryShoot()
    {
        if (_isShotCooldown) return;

        if (_ammo > 0)
        {
            _ammo--;
            Shoot();

            if (_ammo > 0)
            {
                _isShotCooldown = true;
                Invoke(nameof(LooseShotCooldown), _weaponData.ShootCooldown);
            }
            else
            {
                Invoke(nameof(Reload), _weaponData.ReloadTime);
            }
        }
    }

    private void LooseShotCooldown()
    {
        _isShotCooldown = false;
    }

    private void Shoot()
    {
        RaycastHit2D hit = Physics2D.Raycast(_shootingFrom.position, -_shootingFrom.right,10, _layerMask);
        if (hit == false) return;
        if (_layerMask == (_layerMask | (1 << hit.transform.gameObject.layer)))
        {
            hit.transform.gameObject.GetComponent<Health>().TakeDamage(_weaponData.Damage);
        }
    }

    private void Reload()
    {
        _ammo = _weaponData.MaxAmmo;
    }

}
