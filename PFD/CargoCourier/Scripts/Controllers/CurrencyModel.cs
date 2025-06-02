
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

[Serializable]
public class CurrencyModel
{
    public event Action<ulong, ulong> ON_AMOUNT_CHANGE;

    public int GetNominal()
    {
        return 1;
    }
    public string GetDefaultAmount()
    {
        return Config.defaultAmount;
    }
    public string GetID()
    {
        return Config.ID;
    }
    public int GetSpriteID()
    {
        return Config.spriteID;
    }
    public string GetPlayerPrefsKey()
    {
        return Config.playerPrefsKey;
    }
    public GameObject GetSkin()
    {
        return Config.skin;
    }

    public bool CanBuyWithCurrency(ulong _cost)
    {
        bool _canBuy = false;
        if (Amount >= _cost) _canBuy = true;
        return _canBuy;
    }

    [SerializeField]
    private ulong _amount;
    public ulong Amount
    {
        get => _amount;
        set
        {
            if (_amount == value) return;
            ulong _prevValue = _amount;
            _amount = value;
            ON_AMOUNT_CHANGE?.Invoke(_prevValue, _amount);
            SaveCurrency(_amount);
            //Debug.Log("CurrencyModel. Amount: " + GetID() + ". Prev: " + _prevValue + " New: " + _amount);
        }
    }

    [SerializeField]
    private CurrencyConfig _config;
    public CurrencyConfig Config
    {
        get => _config;
        set
        {
            if (_config == value) return;
            _config = value;
        }
    }

    public void LoadCurrency()
    {
        Amount = Convert.ToUInt64(PlayerPrefs.GetString(GetPlayerPrefsKey(), GetDefaultAmount()));
        //Amount = Convert.ToUInt64(PlayerPrefs.GetString(GetPlayerPrefsKey(), ""));
        //Debug.Log("BalanceController. Load: " + GetID() + ". Amount: " + Amount);
    }

    void SaveCurrency(ulong _value)
    {
        //Debug.Log("BalanceController. Save: " + GetID() + ". Amount: " + _value);
        PlayerPrefs.SetString(GetPlayerPrefsKey(), Convert.ToString(_value));
    }
}