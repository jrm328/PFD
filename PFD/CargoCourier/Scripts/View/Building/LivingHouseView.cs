using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingHouseView : BuildingView
{
    protected override void SetBuilding()
    {
        if (app.controller.IsTutorial) return;

        skin.SetActive(true);
        skin.GetComponent<DoAnimations>().DoScaleOutAnimation();
    }
}
