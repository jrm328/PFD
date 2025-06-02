using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CheatPopupWindow : PopupWindowBase
{
    [SerializeField]
    private Button _uiVisibilityButton;
    [SerializeField]
    private CashButton _addCashButton;
    [SerializeField]
    private GemButton _addGemsButton;
    [SerializeField]
    private Button _changeGroundButton;
    [SerializeField]
    private Button _delReloadButton;

    protected override void OnShow()
    {
        base.OnShow();

        closeButton.onClick.AddListener(HideWindow);

        _uiVisibilityButton.onClick.AddListener(OnUiVisibilityButton);
        _addCashButton._button.onClick.AddListener(OnAddCashButton);
        _addGemsButton._button.onClick.AddListener(OnAddGemsButton);
        _changeGroundButton.onClick.AddListener(ChangeGroundButtonClick);
        _delReloadButton.onClick.AddListener(OnDelReloadButtonClick);


        _addCashButton.SetButtonText(_addCashButton.GetTextSprite(_addCashButton.currencyModel.GetSpriteID()), "100");
        _addGemsButton.SetButtonText(_addGemsButton.GetTextSprite(_addGemsButton.currencyModel.GetSpriteID()), "1");
    }

    void OnUiVisibilityButton()
    {
        foreach (GameObject _ui in app.view.gameHud.uiToHide)
        {
            if (_ui.activeInHierarchy) _ui.SetActive(false);
            else _ui.SetActive(true);
        }
    }

    void OnAddCashButton()
    {
        ulong _value = 100;
        app.controller.balanceController.AddCurrency(_addCashButton.currencyModel, _value);
    }
    void OnAddGemsButton()
    {
        ulong _value = 1;
        app.controller.balanceController.AddCurrency(_addGemsButton.currencyModel, _value);
    }

    public void ChangeGroundButtonClick()
    {
        app.controller.groundController.ChangeTexture();
    }

    public void OnDelReloadButtonClick()
    {
        DeletePlayerPrefs();
        ReLoadScene();
    }

    void DeletePlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    void ReLoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    protected override void PlayShowAnimation()
    {
        _rect.DOKill();
        float _scale = 0.85f;
        _rect.localScale = new Vector3(_scale, _scale, _scale);
        _rect.anchoredPosition = GetStartPos();
        _rect.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack).OnComplete(() => { });
    }

    protected override void PlayHideAnimation()
    {
        _rect.DOKill();
        float _scale = 0.8f;
        _rect.DOScale(new Vector3(_scale, _scale, _scale), 0.2f)
            .SetEase(Ease.InBack)
            .OnComplete(() =>
            {
                OnHideAnimationComplete();
            });
    }
}