using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using UnityEngine.Events;
using DG.Tweening;

public class UnitView : GameElement
{
    public event Action ON_CAN_ATTACK;

    public UnitModel model;
    public ParcelModel parcelModel = new ParcelModel();

    public GameObject skinUnit;
    public GameObject skinParcel;

    public UnitSkinSettings unitSkinSettings;
    public UnitAnimation unitAnimation;

    public GameObject _uiParcelsRadar;
    public UIRadarIconSettings uiParcelIconSettings;

    Vector3 _cachedRotation = new Vector3(0f, 180f, 0f);

    private void Start()
    {
        parcelModel.ON_CONFIG_CHANGE += UpdateParcelSkin;
    }

    public virtual void OnCanAttack()
    {
        ON_CAN_ATTACK?.Invoke();
    }

    void OnHealthChange(UnitModel unitModel)
    {
        Debug.Log("UnitView. OnHealthChange. Health: " + unitModel.GetHealth());
    }

    public UnitConfig GetConfig()
    {
        return model.Config;
    }

    protected virtual void OnDied(UnitModel unitModel)
    {
        Destroy(gameObject);
    }

    private void Subscribe()
    {
        model.ON_CONFIG_CHANGE += OnConfigChange;
        model.ON_HEALTH_CHANGE += OnHealthChange;
        model.ON_DIED += OnDied;
    }

    private void OnDestroy()
    {
        model.ON_CONFIG_CHANGE -= OnConfigChange;
        model.ON_HEALTH_CHANGE -= OnHealthChange;
        model.ON_DIED -= OnDied;
    }

    void OnConfigChange(UnitModel _model)
    {
        UpdateSkin(model);

        GetSkinSettings();
        GetWeaponNode();
        GetVehicleNode();
    }

    public void SetModel(UnitModel _model)
    {
        model = _model;
        AddCollider();
        AddRigidBody();
        Subscribe();

        UpdateSkin(model);

        GetSkinSettings();
        GetWeaponNode();
        GetVehicleNode();
    }

    public void FireParcel(ParcelModel _parcelModel, Transform _parcelDestination, UnitView _isFromReceiver)
    {
        if (_parcelModel.Config != null)
        {
            GameObject _skinParcelToFire = Instantiate(_parcelModel.Config.skin);
            _skinParcelToFire.transform.position = GetParcelNode().position;

            ParcelController _parcelController = _skinParcelToFire.GetComponent<ParcelController>();
            _parcelController.FireParcel(_parcelModel.Config, _parcelDestination, _isFromReceiver);
        }
    }

    protected virtual void UpdateSkin(UnitModel _model)
    {
        Debug.Log("UnitView. UpdateView: " + _model.GetID());
        if (_model.Config != null)
        {
            if (skinUnit != null)
            {
                CacheRotation();
                Destroy(skinUnit);
            } 
            skinUnit = Instantiate(_model.Config.skin, transform);

            DoAnimations();
            SetRotation();
        }
    }

    void CacheRotation()
    {
        _cachedRotation = skinUnit.transform.localEulerAngles;
    }

    public Vector3 GetCacheRotation()
    {
        return _cachedRotation;
    }

    protected virtual void SetRotation()
    {   }

    protected virtual void DoAnimations()
    {   }


    protected virtual void GetSkinSettings()
    {
        if (skinUnit != null)
        {
            unitSkinSettings = skinUnit.GetComponent<UnitSkinSettings>();
            unitAnimation = skinUnit.GetComponent<UnitAnimation>();
        }
    }

    public void UpdateParcelSkin(ParcelModel _parcelModel)
    {
        if (skinParcel != null) Destroy(skinParcel);

        if (_parcelModel.Config != null)
        {
            skinParcel = Instantiate(_parcelModel.Config.skin, GetParcelNode());
            skinParcel.transform.localPosition = Vector3.zero;

            unitAnimation.OnUnitGetParcel();
        }
        else
        {
            unitAnimation.OnUnitLostParcel();
        }
    }

    public Transform GetParcelNode()
    {
        return unitSkinSettings.GetParcelNode();
    }


    protected virtual Transform GetWeaponNode()
    {
        return null;
    }

    protected virtual Transform GetVehicleNode()
    {
        return null;
    }

    protected virtual void AddRigidBody()
    {
        var rb = gameObject.AddComponent<Rigidbody>();
        rb.mass = 1;
        rb.linearDamping = Mathf.Infinity;
        rb.angularDamping = Mathf.Infinity;
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
    }

    protected virtual void AddCollider()
    {

    }

    private float _toScale = 0.8f;
    private float _amplitude = 10f;
    private float _durationOut = 0.08f;
    private float _durationIn = 0.26f;

    public void PlayPulsAnimation()
    {
        transform?.DOKill();
        transform?.DOScale(Vector3.one * _toScale, _durationOut).SetEase(Ease.InOutQuad).OnComplete(PlayPulsAnimationOut);
    }

    void PlayPulsAnimationOut()
    {
        transform?.DOKill();
        transform?.DOScale(Vector3.one, _durationIn).SetEase(Ease.OutBack, _amplitude);
    }

    protected virtual void InstantiateUIRadarIcon()
    {
        _uiParcelsRadar = Instantiate(app.view.gameHud.uiRadarIcon, app.view.gameHud.canvasParcelsRadar.transform);
        uiParcelIconSettings = _uiParcelsRadar.GetComponent<UIRadarIconSettings>();
        uiParcelIconSettings.Target = transform;
        uiParcelIconSettings.IconText.text = "<sprite=" + parcelModel.GetSpriteID() + ">";
        uiParcelIconSettings.GlobalVisibility(false);
    }

    public void SetWalkingAnimation(bool value)
    {
        if (unitAnimation != null)
        {
            unitAnimation.IsWalking = value;
        } 
    }

    public virtual void SetAttacking(bool value)
    {

    }
}
