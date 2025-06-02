using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Utilities;

public class BoostsView : GameElement
{
    private void Awake()
    {

    }

    void Start()
    {
        app.controller.boostsController.ON_BOOST_TIME_LEFT_CHANGED += OnBoostTimeLeftChanged;
        app.controller.boostsController.ON_BOOST += OnBoost;

        app.controller.ON_TUTORIAL_CHANGED += OnTutotialChanged;

        OnTutotialChanged(app.controller.IsTutorial);
        OnBoost(app.controller.boostsController.isBoostNow);
    }

    private void OnTutotialChanged(bool _isTutorial)
    {
        if (_isTutorial) app.view.boostsHud.boostHolder.SetActive(false);
        else app.view.boostsHud.boostHolder.SetActive(true);

        OnBoost(app.controller.boostsController.isBoostNow);
    }

    void OnBoostTimeLeftChanged(float _value, float _duration)
    {
        //app.view.boostsHud.boostTimeLeftText.text = "Ends in " + MathUtil.SecondsToMinutes(_value);
        app.view.boostsHud.boostTimeLeftText.text = MathUtil.SecondsToMinutes(_value);
        app.view.boostsHud.barBoostTimeLeft.fillAmount = _value / _duration;
    }

    void OnBoost(bool _status)
    {
        app.view.boostsHud.boostInfoHolder.SetActive(_status);
        app.view.boostsHud.barBoostTimeLeft.gameObject.SetActive(_status);

        //if (_status) app.view.boostsHud.boostIcon.color = Color.yellow;
        //else app.view.boostsHud.boostIcon.color = Color.gray;
    }
}
