using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashCurrencyController : CurrencyController
{
    private void OnEnable()
    {
        currencyModel = app.model.balanceModel.cashModel;
    }

}
