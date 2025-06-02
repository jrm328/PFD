using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
//using Utilities;

[Serializable]
public class UnitModel
{
    public event Action<UnitModel> ON_CONFIG_CHANGE;
    public event Action<UnitModel> ON_HEALTH_CHANGE;
    public event Action<UnitModel> ON_DIED;

    private bool _isDead;
    private bool _isAttacking;



    public string GetID()
    {
        return Config.ID;
    }

    public int GetSpriteID()
    {
        return Config.spriteID;
    }

    public float GetHealth()
    {
        return _health;
    }

    public string GetLocalizationKey()
    {
        return Config.localizationKey;
    }

    public float GetSpeed()
    {
        return Config.speed;
    }

    public float GetRotationSpeed()
    {
        return 10f;
    }

    public bool GetIsDead()
    {
        return _isDead;
    }

    public bool GetIsAttacking()
    {
        return _isAttacking;
    }

    public bool GetIsAvailable()
    {
        bool _isAvailable;
        if (Config.isAvailableByDefault) _isAvailable = true;
        else
        {
            if (PlayerPrefs.GetInt(GetID(), 0) == 1) _isAvailable = true;
            else _isAvailable = false;
        }
        return _isAvailable;
    }

    //public void SetAvailable()
    //{
    //    PlayerPrefs.SetInt(GetID(), 1);
    //}




    [SerializeField]
    private float _health;
    public float Health
    {
        get => _health;
        set
        {
            _health = value;
            ON_HEALTH_CHANGE?.Invoke(this);
            if (_health <= 0f)
            {
                _isDead = true;
                Debug.Log("UnitModel. " + GetID() + " is Dead.");
                ON_DIED?.Invoke(this);
            }
        }
    }
    [SerializeField]
    private UnitConfig _config;
    public UnitConfig Config
    {
        get => _config;
        set
        {
            if (_config==value) return;
            
            _config = value;
            ON_CONFIG_CHANGE?.Invoke(this);
        }
    }


}
