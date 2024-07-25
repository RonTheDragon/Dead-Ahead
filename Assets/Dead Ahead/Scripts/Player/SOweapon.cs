using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Weapon Data")]
public class SOweapon : ScriptableObject
{
    public string WeaponName;
    public int MaxAmmo;
    public float ShootCooldown;
    public float ReloadTime;
    public float Damage;
    public PlayerWeapon WeaponPrefab;
}
