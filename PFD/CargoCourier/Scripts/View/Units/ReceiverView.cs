using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ReceiverView : UnitView
{
    private void Start()
    {
        SetModel(model);

        InstantiateUIRadarIcon();

        app.controller.ON_PLAYER_GET_PARCEL += OnPlayerGetParcel;
        app.controller.ON_PLAYER_LOST_PARCEL += OnPlayerLostParcel;

        SetTargetOnStart();
    }

    protected virtual void SetTargetOnStart()
    {
        SetTarget(app.view.player.transform);
    }

    public GameObject GetUIParcelIcon()
    {
        return _uiParcelsRadar;
    }

    void OnPlayerGetParcel(ParcelConfig parcelConfig)
    {
        if (parcelModel.GetID() == parcelConfig.ID)
        {
            Debug.Log("ReceiverView. OnPlayerGetParcel");
            ParcelMatch(true);
            PlayAnimation(true);
        }
    }

    void OnPlayerLostParcel(ParcelConfig parcelConfig)
    {
        if (parcelModel.GetID() == parcelConfig.ID)
        {
            Debug.Log("ReceiverView. OnPlayerLostParcel");
            ParcelMatch(false);
            PlayAnimation(false);
        }
    }

    protected virtual void ParcelMatch(bool _bool)
    {
        uiParcelIconSettings.GlobalVisibility(_bool);
    }

    protected virtual void PlayAnimation(bool _bool)
    {
        unitAnimation.IsWalking = _bool;
        unitAnimation.IsWaving = _bool;
    }






    protected virtual void  Update()
    {
        LookAt();
    }

    public void LookAt()
    {
        if (Target != null)
        {
            var lookPos = Target.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * model.GetRotationSpeed());
        }
    }

    public void SetTarget(Transform _targ)
    {
        Target = _targ;
    }

    Transform _target;
    public Transform Target
    {
        get => _target;
        set
        {
            if (_target == value) return;
            _target = value;
        }
    }
}

