using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "config/vehicle")]
public class VehicleConfig: ScriptableObject
{
    public GameObject skin;

    public string ID;
    public string localizationKey;

    public Sprite picture;
    public float speed;
}

