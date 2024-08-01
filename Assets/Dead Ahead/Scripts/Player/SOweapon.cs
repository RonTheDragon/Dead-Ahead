using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Weapon Data")]
public class SOweapon : ScriptableObject
{
    public string WeaponName;
    public PlayerWeapon WeaponPrefab;
    public List<WeaponUpgrades> Upgrades;

    [System.Serializable]
    public class WeaponUpgrades
    {
        public int Cost;
        public int MaxAmmo;
        public float ShootCooldown;
        public float ReloadTime;
        public float Damage;
    }
}
