using UnityEngine;
using System;

[Serializable]
public class BalanceModel
{
    [Header("Default Building Cost")]
    public int _defaultBuildingUnlockCost = 100;
    public float _buildingUnlockMult = 1.05f;

    //[Header("Default Flight Cost")]
    //public int _defaultFlightCost = 50;
    //public float _flightCostMult = 1.05f;

    [Header("Parcel")]
    public float parcelSpawnRate = 3f;

    [Header("Gem Milestone")]
    public int gemMilestone = 3;

    [Header("Equipment Rent")]
    public int equipmentRentPrice = 3;
    public float equipmentRentDuration = 120f;

    public CurrencyModel cashModel;
    public CurrencyModel gemModel;
    public CurrencyModel parcelsModel;

    public BalanceModel GetBalanceModel()
    {
        return this;
    }

    public ulong GetBuildingUnlockCost(BuildingConfig buildingConfig)
    {
        ulong _cost = Convert.ToUInt64(_defaultBuildingUnlockCost * Mathf.Pow(_buildingUnlockMult, GetLicenceNumber() - 1));
        return _cost;
    }

    //public ulong GetFlightCost(CityModel cityModel)
    //{
    //    ulong _cost = Convert.ToUInt64(_defaultFlightCost * Mathf.Pow(_flightCostMult, GetFlightNumber(cityModel) - 1));
    //    return _cost;
    //}

    public ulong GetEquipmentRentPrice()
    {
        ulong _cost = Convert.ToUInt64(equipmentRentPrice);
        return _cost;
    }

    public float GetEquipmentRentDuration()
    {
        return equipmentRentDuration;
    }

    int GetFlightNumber(CityModel cityModel)
    {
        return PlayerPrefs.GetInt(cityModel.GetID() + PlayerPrefsKeys.flightNumberKey, 1);
    }
    public void SetFlightNumber(CityModel cityModel)
    {
        int _newNumber = GetFlightNumber(cityModel) + 1;
        PlayerPrefs.SetInt(cityModel.GetID() + PlayerPrefsKeys.flightNumberKey, _newNumber);
    }

    int GetLicenceNumber()
    {
        return PlayerPrefs.GetInt(PlayerPrefsKeys.licenceNumberKey, 1);
    }
    public void SetBuildingUnlockedNumber()
    {
        int _newNumber = GetLicenceNumber() + 1;
        PlayerPrefs.SetInt(PlayerPrefsKeys.licenceNumberKey, _newNumber);
    }
}
