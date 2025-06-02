using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AirportPopupWindow : PopupWindowBase
{
    [SerializeField]
    private TextMeshProUGUI _nameText;
    [SerializeField]
    private Button _closeButton;
    [SerializeField]
    private GameObject _parameterCity;
    [SerializeField]
    private RectTransform _spawnRoot;

    protected override void OnShow()
    {
        base.OnShow();

        _closeButton.onClick.AddListener(HideWindow);

        SetBuildingNameText();

        CreateSlots(app.model.cityModels);
    }

    void SetBuildingNameText()
    {
        string _city = "some city";
        string _building = "some building";
        _nameText.text = _city + " " + _building;
    }

    public void CreateSlots(List<CityModel> cityModels)
    {
        foreach (CityModel cityModel in cityModels)
        {
            if (cityModel.GetID() != app.model.currentCityModel.GetID())
            {
                GameObject parameterCity = Instantiate(_parameterCity, _spawnRoot.transform);
            }
        }
    }

    protected override void PlayShowAnimation()
    {
        _rect.DOKill();
        _rect.anchoredPosition = _startPosition;
        float _scale = 0.85f;
        _rect.localScale = new Vector3(_scale, _scale, _scale);
        _rect.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack).OnComplete(() => {});
    }

    protected override void PlayHideAnimation()
    {
        _rect.DOKill();
        float _scale = 0.85f;
        _rect.DOScale(new Vector3(_scale, _scale, _scale), 0.1f)
            .SetEase(Ease.InBack)
            .OnComplete(() =>{ OnHideAnimationComplete(); });
    }
}