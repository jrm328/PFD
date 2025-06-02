using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoostsController : GameElement
{
    public event Action<float, float> ON_BOOST_TIME_LEFT_CHANGED;
    public event Action<bool> ON_BOOST;

    [HideInInspector]
    public bool isBoostNow;
    //public float boostDuration = 10f;
    private float _totalBoostDuration;
    public float waitTime = 1f;

    void Awake()
    {
        GetBoostTimeLeft();
    }

    private void Start()
    {

    }

    IEnumerator WaitAndstartBoost(UnitConfig _unitConfig)
    {
        BoostTimeLeft = BoostTimeLeft + app.model.balanceModel.GetEquipmentRentDuration();
        _totalBoostDuration = BoostTimeLeft;
        isBoostNow = true;

        yield return new WaitForSeconds(waitTime);

        ON_BOOST?.Invoke(isBoostNow);
        ChangePlayerConfig(_unitConfig);
    }

    public void StartBoost(UnitConfig _unitConfig)
    {
        StartCoroutine(WaitAndstartBoost(_unitConfig));
    }

    void ChangePlayerConfig(UnitConfig unitConfig)
    {
        app.view.GetPlayerView().CurrentPlayerChenged(unitConfig);
    }

    void EndBoost()
    {
        ON_BOOST?.Invoke(isBoostNow);
        ChangePlayerConfig(app.globalConfigs.defaultPlayerConfig);
    }

    void GetBoostTimeLeft()
    {
        BoostTimeLeft = PlayerPrefs.GetFloat("BoostTimeLeft", 0f);
    }

    void Update()
    {
        if (isBoostNow)
        {
            BoostTimeLeft -= Time.deltaTime;
        }
    }

    public float _boostTimeLeft;
    public float BoostTimeLeft
    {
        get => _boostTimeLeft;
        set
        {
            _boostTimeLeft = value;

            if (!isBoostNow) return;
            ON_BOOST_TIME_LEFT_CHANGED?.Invoke(_boostTimeLeft, _totalBoostDuration);

            if (_boostTimeLeft <= 0f)
            {
                isBoostNow = false;
                EndBoost();
            }
        }
    }
}
