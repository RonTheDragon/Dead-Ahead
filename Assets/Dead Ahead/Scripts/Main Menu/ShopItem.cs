using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private TMP_Text _name, _lvlTxt, _priceTxt, _buyText;
    [SerializeField] private Image _used;
    [SerializeField] private Button _selectButton, _buyButton;

    private List<int> _prices;
    private int _currentLevel, _currentWeapon;
    private GameData _gameData;
    private Action _onBuy;

    public void SetUpShopItem(string name, int lvl, List<int> prices,Action onSelect, Action onBuy, GameData data, int weaponIndex)
    {
        _gameData=data;
        _currentWeapon = weaponIndex;
        _name.text = name;
        _currentLevel=lvl;
        _lvlTxt.text = $"lvl{_currentLevel}";
        _prices=prices;
        if (_currentLevel == _prices.Count)
        {
            _buyButton.interactable = false;
            _priceTxt.text = $"MAXED OUT";
        }
        else
        {
            _priceTxt.text = $"{_prices[_currentLevel]}$";
        }

        _selectButton.onClick.AddListener(() => onSelect());
        _selectButton.onClick.AddListener(OnSelect);

        _buyButton.onClick.AddListener(OnBuy);
        _onBuy = onBuy;

        if (_currentLevel == 0)
        {
            _selectButton.interactable = false;
        }
    }

    public void OnDeselect()
    {
        _used.gameObject.SetActive(false);
        if (_currentLevel > 0)
        {
            _selectButton.interactable = true;
        }
    }

    public void OnSelect() 
    {
        _used.gameObject.SetActive(true);
        _selectButton.interactable = false;
        _gameData.CurrentWeaponIndex = _currentWeapon;
    }

    public void OnBuy()
    {
        if(_gameData.Money >= _prices[_currentLevel])
        {
            _gameData.Money-= _prices[_currentLevel];

            _currentLevel++;
            _lvlTxt.text = $"lvl{_currentLevel}";
            if (_currentLevel == 1)
            {
                _selectButton.interactable=true;
            }
            ++_gameData.WeaponLevels[_currentWeapon];
            _onBuy.Invoke();

            if (_currentLevel== _prices.Count)
            {
                _buyButton.interactable=false;
                _priceTxt.text = $"MAXED OUT";
            }
            else
            {
                _priceTxt.text = $"{_prices[_currentLevel]}$";
            }
        }
    }
}
