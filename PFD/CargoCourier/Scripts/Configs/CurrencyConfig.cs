using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[CreateAssetMenu(menuName ="config/currency")]
public class CurrencyConfig: ScriptableObject
{
    public GameObject skin;

    public string ID;
    public int spriteID;
    public string localizationKey;
    public string playerPrefsKey;
    public string defaultAmount;
}


