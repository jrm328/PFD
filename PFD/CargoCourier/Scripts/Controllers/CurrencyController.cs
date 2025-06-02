using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyController : GameElement
{
    [SerializeField]
    public CurrencyModel currencyModel;

    private void OnDestroy()
    {
        app.controller.OnCurrencyDestroyedFireEvent(currencyModel);
    }
}
