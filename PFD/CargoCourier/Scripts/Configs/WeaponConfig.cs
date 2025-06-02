using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[CreateAssetMenu(menuName ="config/weapon")]
public class WeaponConfig: ScriptableObject
{
    public GameObject skin;
    public GameObject bullet;
    public GameObject catridge;

    public string ID;
    public string localizationKey;

    public Sprite picture;

    public bool isAvailable;
    public float damage;

    public float rateOfFire;
    public float rangeOfFire;

    public float accuracyAngle;
    public float accuracyPositionX;

    public float bulletScaleMin;
    public float bulletScaleMax;



}


