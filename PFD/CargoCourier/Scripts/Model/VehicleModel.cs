using UnityEngine;
using UnityEngine.Events;
using System;

[Serializable]
public class VehicleModel
{
    public event Action<VehicleModel> ON_CONFIG_CHANGE;

    [SerializeField]
    private VehicleConfig _config;
    [SerializeField]
    public VehicleConfig Config
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