using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModel : GameElement
{
    public BalanceModel balanceModel = new BalanceModel();
    [HideInInspector]
    public List<CityModel> cityModels = new List<CityModel>();
    [HideInInspector]
    public CityModel defaultCityModel = new CityModel();
    [HideInInspector]
    public CityModel currentCityModel = new CityModel();
}
