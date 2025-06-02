using UnityEngine;
using System;

[Serializable]
public class BoostModel
{
    [Header("Default Land Cost")]
    public int _defaultLicenceCost = 1;
    public float _licenceCostMult = 1.005f;

    [Header("Default Flight Cost")]
    public int _defaultFlightCost = 1;
    public float _flightCostMult = 1.1f;

    [Header("Parcel")]
    public float parcelSpawnRate = 3f;

    [Header("Gem Milestone")]
    public int gemMilestone = 3;

    public CurrencyModel cashModel;
    public CurrencyModel gemModel;
    public CurrencyModel adsModel;
    public CurrencyModel parcelsModel;

    public BoostModel GetBalanceModel()
    {
        return this;
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
}
