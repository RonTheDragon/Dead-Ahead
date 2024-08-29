using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public int Money;
    public int CurrentWeaponIndex;
    public List<int> WeaponLevels;
    public int DistanceRecord, KillsRecord, ComboRecord;
}
