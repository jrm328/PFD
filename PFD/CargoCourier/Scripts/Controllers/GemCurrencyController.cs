using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemCurrencyController : CurrencyController
{
    private void OnEnable()
    {
        currencyModel = app.model.balanceModel.gemModel;
    }
}
