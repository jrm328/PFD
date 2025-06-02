using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSkinSettings : MonoBehaviour
{
    public Transform bodyNode;
    public Transform bodyAnimationNode;
    public Transform weaponNode;
    public Transform parcelNode;
    public Transform vehicleNode;
    public Transform rightHandNode;
    public Transform leftHandNode;

    public Transform GetBodyNode()
    {
        return bodyNode;
    }

    public Transform GetBodyAnimationNode()
    {
        return bodyAnimationNode;
    }

    public Transform GetWeaponNode()
    {
        return weaponNode;
    }

    public Transform GetParcelNode()
    {
        return parcelNode;
    }

    public Transform GetVehicleNode()
    {
        return vehicleNode;
    }

    public Transform GetRightHandNode()
    {
        return rightHandNode;
    }

    public Transform GetLeftHandNode()
    {
        return leftHandNode;
    }
}
