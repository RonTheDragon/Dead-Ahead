using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponsList", menuName = "Weapons List")]
public class SOweaponsList : ScriptableObject
{
    public List<SOweapon> Weapons;
}
