using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSkinSettings : MonoBehaviour
{
    public Transform[] bulletNodes;
    public Transform[] catridgeNodes;

    public Transform[] GetBulletNodes()
    {
        return bulletNodes;
    }

    public Transform[] GetCatridgeNodes()
    {
        return catridgeNodes;
    }
}
