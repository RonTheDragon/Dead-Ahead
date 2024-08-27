using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerScoreSystem : MonoBehaviour, IPlayerComponent
{
    private GameManager _gm;
    private PlayerHealth _playerHealth;
    private int _currentMoneyEarn, _totalMoney, _moneyBeforeGame, _currentCombo, _distance , _totalKills;
    [SerializeField] private float _comboResetTime = 5;
    [SerializeField] private float _comboFadeOutTime = 1;
    private float _comboTimeLeft;
    [SerializeField] private TMP_Text _comboText, _comboLevelText,_totalKillsText, _distanceText, _totalMoneyText, _currentMoneyEarnedText;
    [SerializeField] private float _updateDistanceDelay;
    [SerializeField] private List<ComboLevel> _comboLevels;
    [SerializeField] private int _metersPerDollarWorth;

    private bool _alreadySaved;

    public void PlayerStart(PlayerRefs refs)
    {
        _gm = GameManager.Instance;
        _playerHealth = refs.PlayerHealth;

        _moneyBeforeGame = _gm.SaveSystem.GameData.Money;
        _totalMoney = _moneyBeforeGame;
        _totalKillsText.text = $"{_totalKills.ToString()} Kills";
        UpdateTotalMoneyText();

        StartCoroutine(DistancePassed());
        _comboText.alpha = 0;
        _comboLevelText.alpha = 0;
        _currentMoneyEarnedText.alpha = 0;
    }

    public void KilledEnemy(int Money)
    {
        if (_playerHealth.IsDead) return;

        _currentCombo++;
        _comboText.text = $"+{_currentCombo.ToString()}";
        _comboText.alpha = 1;
        _comboLevelText.text = CaculateComboName();
        _comboLevelText.alpha = 1;

        _currentMoneyEarn += Money + CaculateComboBonus();
        _currentMoneyEarnedText.text = $"+{_currentMoneyEarn}$";
        _currentMoneyEarnedText.alpha = 1;



        if (_comboTimeLeft <= 0)
        {
            StartCoroutine(ResetCombo());
        }
        _comboTimeLeft = _comboResetTime;
    }

    private void OnDisable()
    {
        SaveProgress();
    }

    public void SaveProgress()
    {
        if (_alreadySaved) return;
        _alreadySaved = true;
        _totalMoney += (_distance / _metersPerDollarWorth) + _currentMoneyEarn;
        _gm.SaveSystem.GameData.Money = _totalMoney;
        _gm.SaveSystem.SaveGame();
    }

    private IEnumerator ResetCombo()
    {
        yield return null;
        while (_comboTimeLeft > 0 && !_playerHealth.IsDead)
        {
            _comboTimeLeft-=Time.deltaTime;
            if (_comboTimeLeft < _comboFadeOutTime)
            {
                float alpha = Mathf.Lerp(0, 1, _comboTimeLeft / _comboFadeOutTime);
                _comboText.alpha = alpha;
                _comboLevelText.alpha = alpha;
                _currentMoneyEarnedText.alpha = alpha;
            }
            yield return null;
        }

        _comboText.alpha = 0;
        _comboLevelText.alpha = 0;
        _currentMoneyEarnedText.alpha = 0;

        _totalKills += _currentCombo;
        _totalKillsText.text = $"{_totalKills.ToString()} Kills";
        _currentCombo = 0;

        _totalMoney += _currentMoneyEarn;
        UpdateTotalMoneyText();
        _currentMoneyEarn = 0;


    }

    private IEnumerator DistancePassed()
    {
        while (!_playerHealth.IsDead)
        {
            _distance = (int)transform.position.x;
            _distanceText.text = $"Distance: {_distance.ToString()}m";
            if (_updateDistanceDelay > 0)
            yield return new WaitForSeconds(_updateDistanceDelay);
            yield return null;
        }
    }


    private int CaculateComboBonus()
    {
        int n = GetComboLevel();
        if (n == -1)
        return 0;
        else
        {
            return _comboLevels[n].MoneyBonusPerKill;
        }
    }

    private string CaculateComboName()
    {
        int n = GetComboLevel();
        if (n == -1)
        return "";
        else
        {
            return _comboLevels[n].Name;
        }
    }

    private int GetComboLevel()
    {
        for (int i = _comboLevels.Count - 1; i >= 0; i--)
        {
            if (_comboLevels[i].ComboRequired <= _currentCombo)
            {
                return i;
            }
        }
        return -1;
    }

    private void UpdateTotalMoneyText()
    {
        _totalMoneyText.text = $"{_totalMoney}$";
    }

    [System.Serializable]
    private class ComboLevel
    {
        public string Name;
        public int ComboRequired;
        public int MoneyBonusPerKill;
    }
}
