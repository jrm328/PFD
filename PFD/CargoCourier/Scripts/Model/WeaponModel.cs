using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
//using Utilities;

[Serializable]
public class WeaponModel
{
    public event Action<WeaponModel> ON_CONFIG_CHANGE;

    public bool hasConfig;

    [SerializeField]
    private WeaponConfig _config;

    public float GetRateOfFire()
    {
        return Config.rateOfFire;
    }

    public float GetRangeOfFire()
    {
        return Config.rangeOfFire;
    }

    public float GetAccuracyAngle()
    {
        return Config.accuracyAngle;
    }

    public float GetAccuracyPositionX()
    {
        return Config.accuracyPositionX;
    }

    public float GetBulletScaleMin()
    {
        return Config.bulletScaleMin;
    }

    public float GetBulletScaleMax()
    {
        return Config.bulletScaleMax;
    }

    public GameObject GetBullet()
    {
        return Config.bullet;
    }



    public string GetID()
    {
        return Config.ID;
    }

    public float GetDamage()
    {
        return Config.damage;
    }

    public string GetLocalizationKey()
    {
        return Config.localizationKey;
    }

    public bool GetIsAvailable()
    {
        return Config.isAvailable;
    }

    public WeaponConfig Config
    {
        get => _config;
        set
        {
            if (_config == value) return;

            _config = value;
            hasConfig = true;
            ON_CONFIG_CHANGE?.Invoke(this);
        }
    }


}
