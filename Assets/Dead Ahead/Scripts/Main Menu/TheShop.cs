using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TheShop : MonoBehaviour
{
    [SerializeField] private SOweaponsList _weapons;
    [SerializeField] private RectTransform _weaponsShopContent;
    [SerializeField] private ShopItem _shopItem;
    [SerializeField] private TMP_Text _moneyUI;
    private GameData _gameData;
    private List<ShopItem> _weaponsInShop;

    public void SetUpShop(GameData gameData)
    {
        _weaponsInShop = new List<ShopItem>();

        _gameData = gameData;
        _gameData.Money = 200;
        _moneyUI.text = $"{_gameData.Money}$";
        if (_gameData.WeaponLevels == null)
        {
            ResetData();
        }
        else if (_gameData.WeaponLevels.Count ==0)
        {
            ResetData();
        }

        int n = 0;
        foreach (SOweapon weapon in _weapons.Weapons) 
        {
            ShopItem weaponItem = Instantiate(_shopItem, transform.position, Quaternion.identity, _weaponsShopContent);

            List<int> prices = new List<int>();
            foreach(SOweapon.WeaponUpgrades upgrade in weapon.Upgrades)
            {
                prices.Add(upgrade.Cost);
            }
            weaponItem.SetUpShopItem(weapon.WeaponName,gameData.WeaponLevels[n], prices, () => SelectWeapon(),  () => UpgradeWeapon(),_gameData,n);
            _weaponsInShop.Add(weaponItem);
            n++;
        }
        _weaponsInShop[_gameData.CurrentWeaponIndex].OnSelect();
    }

    private void ResetData()
    {
        _gameData.WeaponLevels = new List<int>();
        for (int i = 0; i < _weapons.Weapons.Count; i++)
        {
            _gameData.WeaponLevels.Add(0);
        }
        _gameData.WeaponLevels[0] = 1; //set Pistol to baught
    }

    private void UpgradeWeapon()
    {
        _moneyUI.text = $"{_gameData.Money}$";
    }

    private void SelectWeapon() 
    {
        foreach(ShopItem item in _weaponsInShop)
        {
            item.OnDeselect();
        }
    }
}
