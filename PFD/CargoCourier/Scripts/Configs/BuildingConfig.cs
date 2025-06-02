using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "config/building")]
public class BuildingConfig: ScriptableObject
{
    public bool availableByDefault;

    //public GameObject skin;

    public string ID;
    public string localizationKey;

    public Sprite picture;

    public int spriteID;
    //public int buildingGemsCount;
}