using System.Collections.Generic;
using UnityEngine;


public class GlobalConfigs: GameElement
{
    [Header("Cities")]
    public List<CityConfig> cityConfigs;
    public CityConfig defaultCityConfig;
    [Header("Players")]
    public List<UnitConfig> playerConfigs;
    public UnitConfig defaultPlayerConfig;
    [Header("Weapons")]
    public List<WeaponConfig> weaponConfigs;
    [Header("Enemies")]
    public List<UnitConfig> enemyConfigs;
    [Header("Senders")]
    public List<UnitConfig> senderConfigs;
    [Header("Parcels")]
    public List<ParcelConfig> parcelConfigs;
    //[Header("Vehicles")]
    //public List<VehicleConfig> vehicleConfigs;
}

