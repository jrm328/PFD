using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;

public class PlayerView : UnitView
{
    public event Action<Transform> ON_NODE_FOR_WEAPON_CREATED;
    public event Action<Transform> ON_NODE_FOR_VEHICLE_CREATED;

    public override void OnCanAttack()
    {
        base.OnCanAttack();
    }

    protected override void AddRigidBody()
    {

    }

    private void Start()
    {
        parcelModel.ON_CONFIG_CHANGE += UpdateParcelSkin;

        app.controller.ON_PLAYER_GET_PARCEL += OnPlayerGetParcel;
        app.controller.ON_PLAYER_LOST_PARCEL += OnPlayerLostParcel;

        app.model.currentCityModel.ON_CONFIG_CHANGED += CurrentCityChenged;
        
        GetComponent<PlayerMovement>().ON_PLAYER_WALK += SetWalkingAnimation;
    }

    private void CurrentCityChenged(CityModel _cityModel)
    {
        OnPlayerLostParcelFireEvent();
    }

    public void CurrentPlayerChenged(UnitConfig _newPlayerConfig)
    {
        model.Config = _newPlayerConfig;
        OnPlayerLostParcelFireEvent();
    }

    void OnPlayerLostParcelFireEvent()
    {
        if (parcelModel.Config)
        {
            app.controller.OnPlayerLostParcelFireEvent(parcelModel.Config);
        } 
    }

    void OnPlayerGetParcel(ParcelConfig parcelConfig)
    {
        foreach (ParcelConfig _parcelConfig in app.globalConfigs.parcelConfigs)
        {
            if (_parcelConfig.ID == parcelConfig.ID)
            {
                parcelModel.Config = _parcelConfig;
                PlayPulsAnimation();
                unitAnimation.HasParcel = true;
                break;
            }
        }
        if(!parcelModel.Config) Debug.LogError("WARNING. PARCEL " + parcelConfig.ID + " WAS NOT FOUND.");
    }

    void OnPlayerLostParcel(ParcelConfig parcelConfig)
    {
        parcelModel.Config = null;
        unitAnimation.HasParcel = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Currency"))
        {
            //Debug.LogError("Currency");
            Destroy(other.gameObject);
        }
        if (other.CompareTag("Receiver"))
        {
            //Debug.LogError("Receiver");
            if (PlayerHasParcel())
            {
                ReceiverView _view = other.GetComponent<ReceiverView>();
                if (_view.parcelModel.GetID() == parcelModel.GetID())
                {
                    FireParcel(parcelModel, other.transform, _view);
                    app.controller.OnPlayerLostParcelFireEvent(parcelModel.Config);
                }
            }
        }
        if (other.CompareTag("Sender"))
        {
            //Debug.LogError("Sender");
            if (!PlayerHasParcel())
            {
                SenderView _view = other.GetComponent<SenderView>();
                if (_view.skinParcel)
                {
                    _view.FireParcel(_view.parcelModel, GetParcelNode(), null);
                    ParcelModel _emptyParcelModel = new ParcelModel();
                    _view.UpdateParcelSkin(_emptyParcelModel);
                }
            }
        }
        if (other.CompareTag("Policeman"))
        {
            //Debug.LogError("Policeman");
            if (PlayerHasParcel())
            {
                PolicemanView _view = other.GetComponent<PolicemanView>();
                if (_view.parcelModel.GetID() == parcelModel.GetID())
                {
                    OnPlayerLostParcelFireEvent();
                }
            }
        }
    }

    public bool PlayerHasParcel()
    {
        return parcelModel.Config;
    }

    protected override void OnDied(UnitModel unitModel)
    {
        base.OnDied(unitModel);
        app.controller.ReloadScene();
    }

    protected override Transform GetWeaponNode()
    {
        if (unitSkinSettings != null)
        {
            Debug.Log("UnitView. ON_WEAPON_JOINT_CREATED.");
            ON_NODE_FOR_WEAPON_CREATED?.Invoke(unitSkinSettings.GetWeaponNode());
            return unitSkinSettings.GetWeaponNode();
        }
        return null;
    }

    protected override Transform GetVehicleNode()
    {
        if (unitSkinSettings != null)
        {
            Debug.Log("UnitView. ON_NODE_FOR_VEHICLE_CREATED.");
            ON_NODE_FOR_VEHICLE_CREATED?.Invoke(unitSkinSettings.GetVehicleNode());
            return unitSkinSettings.GetVehicleNode();
        }
        return null;
    }

    public Transform GetBody()
    {
        if (unitSkinSettings != null) return unitSkinSettings.GetBodyNode();
        else return null;
    }

    protected override void DoAnimations()
    {
        GetComponent<DoAnimations>().DoScaleOutAnimation();
    }

    protected override void SetRotation()
    {
        skinUnit.transform.localEulerAngles = GetCacheRotation();
    }
}
