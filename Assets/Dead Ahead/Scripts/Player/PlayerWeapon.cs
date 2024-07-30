using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private SOweapon _weaponData;
    [SerializeField] private ParticleSystem _particleSystem;
    private int _ammo;
    private bool _isShotCooldown;
    private Transform _shootingFrom;
    private LayerMask _layerMask;
    private Image _reloadImage;
    private TMP_Text _ammoText;

    public void SetupWeapon(Transform shootingFrom,LayerMask attackLayerMask,Image reloadImage, TMP_Text ammoText)
    {
        _ammo = _weaponData.MaxAmmo;
        _isShotCooldown = false;
        _shootingFrom = shootingFrom;
        _layerMask = attackLayerMask;

        _reloadImage = reloadImage;
        _ammoText = ammoText;
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
                Invoke(nameof(LooseShotCooldown), _weaponData.ShootCooldown);
            }
            else
            {
                Invoke(nameof(Reload), _weaponData.ReloadTime);
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

        RaycastHit2D hit = Physics2D.Raycast(_shootingFrom.position, -_shootingFrom.right,10, _layerMask);
        if (hit == false) return;
        if (_layerMask == (_layerMask | (1 << hit.transform.gameObject.layer)))
        {
            hit.transform.gameObject.GetComponent<Health>().TakeDamage(_weaponData.Damage);
        }
    }

    private IEnumerator Reload()
    {
        float n = 0;
        while (n< _weaponData.ReloadTime)
        {
            yield return null;
            n += Time.deltaTime;
            _reloadImage.fillAmount = n/_weaponData.ReloadTime;
        }
        _reloadImage.fillAmount = 0;
        _ammo = _weaponData.MaxAmmo;
        UpdateAmmoText();
    }

    private void UpdateAmmoText()
    {
        _ammoText.text = $"{_ammo} / {_weaponData.MaxAmmo}";
    }
}
