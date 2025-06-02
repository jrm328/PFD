using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[CreateAssetMenu(menuName ="config/unit")]
public class UnitConfig: ScriptableObject
{
    public GameObject skin;

    public string ID;
    public string localizationKey;

    //public Sprite picture;

    public bool isAvailableByDefault;
    public float speed;
    public int spriteID;
}


