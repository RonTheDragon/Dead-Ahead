using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private SOweapon _weaponData;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private Transform _weaponModel;
    private int _ammo;
    private bool _isShotCooldown;
    private Transform _shootingFrom;
    private LayerMask _layerMask;
    private Image _reloadImage;
    private TMP_Text _ammoText;
    private int _weaponLevel;

    public void SetupWeapon(PlayerCombat playerCombat)
    {
        _weaponLevel = playerCombat.WeaponLevel;
        _ammo = _weaponData.Upgrades[_weaponLevel].MaxAmmo;
        _isShotCooldown = false;
        _shootingFrom = playerCombat.WeaponShootFrom;
        _layerMask = playerCombat.AttackLayerMask;

        _reloadImage = playerCombat.ReloadImage;
        _ammoText = playerCombat.AmmoText;
        UpdateAmmoText();   
    }

    public bool TryShoot()
    {
        if (_isShotCooldown) return false;

        if (_ammo > 0)
        {
            _ammo--;
            UpdateAmmoText();
            Shoot();

            if (_ammo > 0)
            {
                _isShotCooldown = true;
                Invoke(nameof(LooseShotCooldown), _weaponData.Upgrades[_weaponLevel].ShootCooldown);
            }
            else
            {
                Invoke(nameof(Reload), _weaponData.Upgrades[_weaponLevel].ReloadTime);
                StartCoroutine(nameof(Reload));
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    private void LooseShotCooldown()
    {
        _isShotCooldown = false;
    }

    private void Shoot()
    {
        _particleSystem?.Play();

        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(_layerMask);
        filter.useTriggers = true;

        RaycastHit2D[] hits = new RaycastHit2D[1];
        int hitCount = Physics2D.Raycast(_shootingFrom.position, -_shootingFrom.right, filter, hits, 10);

        if (hitCount > 0)
        {
            Health h = hits[0].transform.gameObject.GetComponent<Health>();
            if (h != null)
            {
                h.TakeDamage(_weaponData.Upgrades[_weaponLevel].Damage);
            }
        }
    }


    private IEnumerator Reload()
    {
        float n = 0;
        while (n< _weaponData.Upgrades[_weaponLevel].ReloadTime)
        {
            yield return null;
            n += Time.deltaTime;
            _reloadImage.fillAmount = n/_weaponData.Upgrades[_weaponLevel].ReloadTime;
        }
        _reloadImage.fillAmount = 0;
        _ammo = _weaponData.Upgrades[_weaponLevel].MaxAmmo;
        UpdateAmmoText();
    }

    private void UpdateAmmoText()
    {
        _ammoText.text = $"{_ammo} / {_weaponData.Upgrades[_weaponLevel].MaxAmmo}";
    }

    public float LowerWeaponAfterTime => _weaponData.Upgrades[_weaponLevel].ShootCooldown+0.01f;

    public Transform WeaponModel => _weaponModel;
}
