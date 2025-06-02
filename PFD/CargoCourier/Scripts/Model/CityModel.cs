using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

[Serializable]
public class CityModel
{
    public event Action<CityModel> ON_CONFIG_CHANGED;

    public string GetID()
    {
        return Config.ID;
    }

    public Vector3 GetAirportPlayerPosition()
    {
        return Config.airportPlayerPosition;
    }

    public string GetLocalizationKey()
    {
        return Config.localizationKey;
    }

    public bool GetIsComingSoon()
    {
        return Config.isComingSoon;
    }

    [SerializeField]
    private CityConfig _config;
    public CityConfig Config
    {
        get => _config;
        set
        {
            if (_config == value) return;
            _config = value;
            ON_CONFIG_CHANGED?.Invoke(this);
        }
    }
}