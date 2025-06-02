using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GemButton : CurrencyButton
{
    protected override void OnEnable()
    {
        base.OnEnable();
        currencyModel = app.model.balanceModel.gemModel;
    }
}
