using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Utilities;

public class BuildingLockedPopupWindow : PopupWindowBase
{
    [SerializeField]
    private TextMeshProUGUI _nameText;
    private bool _isTransaction;
    [SerializeField]
    private CashButton _currencyButton;
    [SerializeField]
    private TextMeshProUGUI _buildingStatusText;
    private BuildingModel _model;

    public void SetBuildingModel(BuildingModel model)
    {
        _model = model;
        _nameText.text = _model.GetID();
    }

    protected override void OnShow()
    {
        base.OnShow();

        closeButton.onClick.AddListener(HideWindow);
        _currencyButton._button.onClick.AddListener(OnClickCurrencyButton);

        CurrencyButtonInteraction();
        CurrencyButtonText();

        _currencyButton.currencyModel.ON_AMOUNT_CHANGE += OnCurrencyChange;
    }

    private void OnDestroy()
    {
        closeButton.onClick.RemoveListener(HideWindow);
        _currencyButton._button.onClick.RemoveListener(OnClickCurrencyButton);

        _currencyButton.currencyModel.ON_AMOUNT_CHANGE -= OnCurrencyChange;
    }

    void OnCurrencyChange(ulong _prevValue, ulong _value)
    {
        CurrencyButtonInteraction();
        CurrencyButtonText();
    }

    public void CurrencyButtonInteraction()
    {
        if (_currencyButton.currencyModel.CanBuyWithCurrency(GetBuildingUnlockCost())) _currencyButton.SetButtonInteraction(true);
        else _currencyButton.SetButtonInteraction(false);
    }
    public void CurrencyButtonText()
    {
        string _textAction = "Unlock";
        string _textCost = MathUtil.UlongToString(GetBuildingUnlockCost());
        _currencyButton.SetButtonText(_textAction, _currencyButton.GetTextSprite(_currencyButton.currencyModel.GetSpriteID()), _textCost);
    }

    ulong GetBuildingUnlockCost()
    {
        return app.model.balanceModel.GetBuildingUnlockCost(_model.Config);
    }

    void OnClickCurrencyButton()
    {
        _isTransaction = true;
        StartCoroutine(WaitAndHide());
    }

    IEnumerator WaitAndHide()
    {
        yield return new WaitForEndOfFrame();
        HideWindow();
    }

    protected override void OnHideAnimationComplete()
    {
        base.OnHideAnimationComplete();
        if (_isTransaction)
        {
            app.controller.balanceController.MinusCurrency(_currencyButton.currencyModel, GetBuildingUnlockCost());
            app.model.balanceModel.SetBuildingUnlockedNumber();
            _model.SetBeckomeUnlocked();
        }
    }


    protected override void PlayShowAnimation()
    {
        _rect.DOKill();
        _rect.anchoredPosition = _startPosition;
        _rect
            .DOAnchorPos3D(GetStartPos(), 0).OnComplete(() =>
            {
                _rect
                    .DOAnchorPos(Vector2.zero, .25f)
                    .OnComplete(() => { })
                    .SetEase(Ease.OutBack);
            });
    }

    protected override void PlayHideAnimation()
    {
        _rect.DOKill();
        _rect
            .DOAnchorPos(GetStartPos(), .25f)
            .OnComplete(() => { OnHideAnimationComplete(); })
            .SetEase(Ease.InBack);
    }
}