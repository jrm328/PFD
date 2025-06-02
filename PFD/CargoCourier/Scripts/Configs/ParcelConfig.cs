using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "config/parcel")]
public class ParcelConfig : ScriptableObject
{
    public GameObject skin;

    public int cost;

    public string ID;
    public string localizationKey;

    public int spriteID;
    public float weight;

    public List<UnitConfig> enemyConfigs;
}