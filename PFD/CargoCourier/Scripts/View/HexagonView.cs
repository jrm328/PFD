using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonView : GameElement
{
    public List<CityView> _cityHexagones;

    void Start()
    {
        app.model.currentCityModel.ON_CONFIG_CHANGED += UpdateHexagon;
        GetHexagones(); //можно вручную складывать все гексы
        UpdateHexagon(app.model.currentCityModel);
    }

    private void GetHexagones()
    {
        foreach (Transform hexagon in transform)
        {
            _cityHexagones.Add(hexagon.GetComponent<CityView>());
        }
    }
    private void UpdateHexagon(CityModel _currentCityModel)
    {
        if (_cityHexagones.Count == 0) return;
        foreach (CityView hexagon in _cityHexagones)
        {
            if(hexagon.model.GetID() == _currentCityModel.GetID()) hexagon.gameObject.SetActive(true);
            else hexagon.gameObject.SetActive(false);
        }
    }
}
