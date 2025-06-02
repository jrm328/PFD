using UnityEngine;

[CreateAssetMenu(menuName ="config/city")]
public class CityConfig: ScriptableObject
{
    public string ID;
    public string localizationKey;
    public Sprite picture;
    public bool isComingSoon;
    public Vector3 airportPlayerPosition;
}


