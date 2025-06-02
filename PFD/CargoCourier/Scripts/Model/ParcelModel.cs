using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

[Serializable]
public class ParcelModel
{
    public event Action<ParcelModel> ON_CONFIG_CHANGE;

    public string GetID()
    {
        return Config.ID;
    }

    public int GetCost()
    {
        return Config.cost;
    }

    public int GetSpriteID()
    {
        return Config.spriteID;
    }

    public UnitConfig GetEnemyConfig()
    {
        if (Config.enemyConfigs.Count == 0) return null;
        return Config.enemyConfigs[Random.Range(0, Config.enemyConfigs.Count)];
    }

    [SerializeField]
    private ParcelConfig _config;
    public ParcelConfig Config
    {
        get => _config;
        set
        {
            if (_config == value) return;
            _config = value;
            ON_CONFIG_CHANGE?.Invoke(this);
        }
    }
}