using UnityEngine;
using System;

public class BalanceController: GameElement
{
    public BalanceModel _balanceModel
    {
        get => app.model.balanceModel;
        set
        {
            app.model.balanceModel = value;
        }
    }

    private void Awake()
    {
        app.controller.ON_CURRENCY_DESTROYED += OnCurrencyDestroyed;
        app.controller.ON_PARCEL_DELIVERED += OnParcelDelivered;

        _balanceModel = app.model.balanceModel.GetBalanceModel();
    }

    public void OnCurrencyDestroyed(CurrencyModel currencyModel)
    {
        ulong _amount = currencyModel.Amount;
        ulong _value = Convert.ToUInt64(currencyModel.GetNominal());
        ulong _newAmount = _amount + _value;
        currencyModel.Amount = _newAmount;
    }

    public void OnParcelDelivered(ParcelModel parcelModel, Vector3 vector3)
    {
        ulong _amount = _balanceModel.parcelsModel.Amount;
        ulong _value = Convert.ToUInt64(_balanceModel.parcelsModel.GetNominal());
        ulong _newAmount = _amount + _value;
        _balanceModel.parcelsModel.Amount = _newAmount;
    }

    public bool ReachedGemMilestone()
    {
        bool _value = false;
        if (PlayerPrefs.GetInt("very_first_parcel", 0) == 0)
        {
            PlayerPrefs.SetInt("very_first_parcel", 1);
            _value = true;
        } 
        else if (_balanceModel.parcelsModel.Amount % Convert.ToUInt64(_balanceModel.gemMilestone) == 0) _value = true;
        return _value;
    }

    public void MinusCurrency(CurrencyModel currencyModel, ulong _value)
    {
        Debug.Log("BalanceController. Minus: " + currencyModel.GetID() + ". Amount: " + currencyModel.Amount + ". Cost: " + _value);
        ulong _amount = currencyModel.Amount;
        ulong _newAmount = _amount - _value;
        currencyModel.Amount = _newAmount;
    }

    public void AddCurrency(CurrencyModel currencyModel, ulong _value)
    {
        ulong _amount = currencyModel.Amount;
        ulong _newAmount = _amount + _value;
        currencyModel.Amount = _newAmount;
    }

    public void LoadBalance()
    {
        _balanceModel.cashModel.LoadCurrency();
        _balanceModel.gemModel.LoadCurrency();
        _balanceModel.parcelsModel.LoadCurrency();
    }
}
