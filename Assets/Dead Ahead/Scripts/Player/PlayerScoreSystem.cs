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
    [SerializeField]
    private TMP_Text _killsTotalDS, _killsCrashDS, _killsKilledDS, _KillsRunOverDS, _KillBossDS, _moneyTotalDS, _moneyEarnedDS,
        _moneyKillsDS, _moneyComboDS, _moneyDistanceDS, _distanceDS, _bestComboDS, _distanceRecordDS, _killsRecordDS, _comboRecordDS;

    private bool _alreadySaved;

    private int _bestCombo, _crashKills,_shotKills,_runoverKills, _bossKills, _moneyFromKills, _moneyFromCombos, _moneyFromDistance;

    private GameData _gameData;

    public void PlayerStart(PlayerRefs refs)
    {
        _gm = GameManager.Instance;
        _playerHealth = refs.PlayerHealth;
        _gameData = _gm.SaveSystem.GameData;
        _moneyBeforeGame = _gameData.Money;
        _totalMoney = _moneyBeforeGame;
        _totalKillsText.text = $"{_totalKills.ToString()} Kills";
        UpdateTotalMoneyText();

        StartCoroutine(DistancePassed());
        _comboText.alpha = 0;
        _comboLevelText.alpha = 0;
        _currentMoneyEarnedText.alpha = 0;
    }

    public void KilledEnemy(int Money,KillInfo.KillType killType)
    {
        if (_playerHealth.IsDead) return;

        switch (killType)
        {
            case KillInfo.KillType.Crash: _crashKills++; break;
            case KillInfo.KillType.Killed: _shotKills++; break;
            case KillInfo.KillType.Runover: _runoverKills++; break;
            case KillInfo.KillType.Boss: _bossKills++; break;
        }

        _currentCombo++;
        if (_bestCombo < _currentCombo) { _bestCombo = _currentCombo; }
        _comboText.text = $"+{_currentCombo.ToString()}";
        _comboText.alpha = 1;
        _comboLevelText.text = CaculateComboName();
        _comboLevelText.alpha = 1;

        int comboBonus = CaculateComboBonus();

        _currentMoneyEarn += Money + comboBonus;
        _currentMoneyEarnedText.text = $"+{_currentMoneyEarn}$";
        _currentMoneyEarnedText.alpha = 1;

        _moneyFromKills += Money;
        _moneyFromCombos += comboBonus;



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

    private void SaveProgress()
    {
        if (_alreadySaved) return;
        _alreadySaved = true;
        _moneyFromDistance = _distance / _metersPerDollarWorth;
        _totalMoney += _moneyFromDistance + _currentMoneyEarn;
        _gm.SaveSystem.GameData.Money = _totalMoney;
        HandleRecords();
        _gm.SaveSystem.SaveGame();
    }

    public void ScoreBoard()
    {
        SaveProgress();
        _killsTotalDS.text = $"In Total: {_totalKills}";
        _killsCrashDS.text = $"Crashed: {_crashKills}";
        _killsKilledDS.text = $"Killed: {_shotKills}";
        _KillsRunOverDS.text = $"Ranover: {_runoverKills}";
        _KillBossDS.text = $"Bosses Defeated: {_bossKills}";

        _moneyTotalDS.text = $"You Now Have: {_totalMoney}$";
        _moneyEarnedDS.text = $"In Total: {_totalMoney-_moneyBeforeGame}$";
        _moneyKillsDS.text = $"Kills Reward: {_moneyFromKills}$";
        _moneyComboDS.text = $"Combo Reward: {_moneyFromCombos}$";
        _moneyDistanceDS.text = $"Distace Reward: {_moneyFromDistance}$";

        _distanceDS.text = $"Distance: {_distance}m";
        _bestComboDS.text = $"Best Combo: {_bestCombo}";
    }

    private void HandleRecords()
    {
        UpdateRecord(ref _gameData.DistanceRecord, _distance, _distanceRecordDS, "Distance Record: ", "m");
        UpdateRecord(ref _gameData.KillsRecord, _totalKills, _killsRecordDS, "Kills Record: ");
        UpdateRecord(ref _gameData.ComboRecord, _bestCombo, _comboRecordDS, "Combo Record: ");
    }

    private void UpdateRecord(ref int recordValue, int currentValue, TMP_Text displayText, string label, string suffix = "")
    {
        bool recordBroken = recordValue < currentValue;
        displayText.text = $"{label}{recordValue}{suffix}{(recordBroken ? " (Broken)" : "")}";
        if (recordBroken) recordValue = currentValue;
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
