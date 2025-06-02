using UnityEngine;
using System.Collections;
using System;


public class VehicleView: GameElement
{
    public GameObject skin;
    public VehicleModel model;

    internal void SetModel(VehicleModel vehicleModel, Transform nodeForWeapon)
    {
        model = vehicleModel;
        //Subscribe();

        Parent(nodeForWeapon);
        ResetTransform();

        UpdateView(model);
    }

    private void UpdateView(VehicleModel vehicleModel)
    {
        Debug.Log("WeaponView. UpdateView: " + vehicleModel.Config.ID);
        if (vehicleModel.Config != null)
        {
            if (skin != null) Destroy(skin.gameObject);
            skin = Instantiate(model.Config.skin, transform);

            //GetSkinSettings();
        }
    }

    private void ResetTransform()
    {
        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.one;
        transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
    }

    private void Parent(Transform nodeForWeapon)
    {
        transform.parent = nodeForWeapon;
    }
}
