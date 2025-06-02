using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using UnityEngine.Events;

public class AirportView : BuildingView
{
    public GameObject _uiAirportRadar;
    UIRadarIconSettings uiParcelIconSettings;

    protected override void SetBuilding()
    {
        base.SetBuilding();

        InstantiateUIRadarIcon();
    }

    private void OnEnable()
    {
        UIRadarVisibility(true);
    }
    private void OnDisable()
    {
        UIRadarVisibility(false);
    }

    protected override void OnPlayerEnterBuilding()
    {
        FirePopup();
    }

    protected override void FirePopup()
    {
        app.controller.popupWindowController.ShowWindow("airport_popup");
    }

    //protected override void SetBuilding()
    //{
    //    if (app.controller.IsTutorial) return;
    //    skin.SetActive(true);
    //    skin.GetComponent<DoBuildingAnimations>().DoScaleOutAnimation();
    //}

    void InstantiateUIRadarIcon()
    {
        if (!app.controller.IsTutorial)
        {
            if (_uiAirportRadar) return;
            _uiAirportRadar = Instantiate(app.view.gameHud.uiRadarIcon, app.view.gameHud.canvasParcelsRadar.transform);
            uiParcelIconSettings = _uiAirportRadar.GetComponent<UIRadarIconSettings>();
            uiParcelIconSettings.Target = transform;
            uiParcelIconSettings.IconText.text = "<sprite=" + model.GetSpriteID() + ">";

        }
    }

    void UIRadarVisibility(bool value)
    {
        if(uiParcelIconSettings) uiParcelIconSettings.GlobalVisibility(value);
    }
}
