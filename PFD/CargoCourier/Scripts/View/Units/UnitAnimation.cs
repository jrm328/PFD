using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimation : MonoBehaviour
{
    private UnitSkinSettings unitSkinSettings;

    public bool animateHands;

    private float _dividerGlobal;
    private float _koefGlobal;

    private float _dividerSpeedGlobal;
    private float _koefSpeedGlobal;

    private bool _hasParcel;
    public bool _isWalking;
    private bool _isWaving;
    private bool _isAttacking;
    private bool _isInShop;

    [SerializeField]
    private float _amplScale = 0.2f;
    private float scaleSpeed = 5f;
    private float scaleSpeedParcel = 7f;

    [SerializeField]
    private float amplPosX = 0.1f;
    [SerializeField]
    private float amplPosY = 0.5f;
    [SerializeField]
    private float _speed = 10f;
    [SerializeField]
    private float _amplRotZ = 8f;
    [SerializeField]
    private float _amplRotXHand = 90f;

    private float index;

    Vector3 _startBodyPosition;
    Vector3 _startBodyRotation;
    Vector3 _startParcelPosition;

    Vector3 _startRHandRotation = Vector3.zero;
    Vector3 _startLHandRotation = Vector3.zero;

    public List<VehicleAnimation> vehicleNodes;

    private void OnEnable()
    {
        _dividerGlobal = 0.5f;
        _dividerSpeedGlobal = 0.75f;

        unitSkinSettings = GetComponent<UnitSkinSettings>();

        _startBodyPosition = unitSkinSettings.GetBodyAnimationNode().localPosition;
        _startBodyRotation = unitSkinSettings.GetBodyAnimationNode().localEulerAngles;

        if (unitSkinSettings.GetParcelNode()) _startParcelPosition = unitSkinSettings.GetParcelNode().localPosition;
    }

    public void OnUnitGetParcel()
    {
        if (animateHands)
        {
            unitSkinSettings.GetRightHandNode().localEulerAngles = _startRHandRotation - new Vector3(90f, 0f, 0f);
            unitSkinSettings.GetLeftHandNode().localEulerAngles = _startLHandRotation - new Vector3(90f, 0f, 0f);
        }
    }

    public void OnUnitLostParcel()
    {
        if (animateHands)
        {
            unitSkinSettings.GetRightHandNode().localEulerAngles = _startRHandRotation;
            unitSkinSettings.GetLeftHandNode().localEulerAngles = _startLHandRotation;
        }
    }

    private void Update()
    {
        index += Time.deltaTime;

        if (IsWalking)
        {
            if (vehicleNodes.Count > 0)
            {
                foreach(VehicleAnimation vehicle in vehicleNodes) vehicle.PlayVehicleAnimation = true;
            } 

            _koefGlobal = 1f;
            _koefSpeedGlobal = 1f;

            float x = amplPosX * Mathf.Cos(_speed * _koefSpeedGlobal * index);
            float y = Mathf.Abs(amplPosY * Mathf.Sin(_speed * _koefSpeedGlobal * index));

            float zRotation = _amplRotZ * Mathf.Cos(_speed * _koefSpeedGlobal * index);

            Vector3 _newBodyPosition = (new Vector3(x, y, 0f) * _koefGlobal);
            Vector3 _newBodyRotation = new Vector3(0f, 0f, zRotation) * _koefGlobal;

            unitSkinSettings.GetBodyAnimationNode().localPosition = _newBodyPosition + _startBodyPosition;
            unitSkinSettings.GetBodyAnimationNode().localEulerAngles = _newBodyRotation + _startBodyRotation;
            unitSkinSettings.GetBodyAnimationNode().localScale = Vector3.one;

            if (animateHands && !HasParcel)
            {
                float xRotationHand = _amplRotXHand * Mathf.Cos(_speed * _koefSpeedGlobal * index);
                Vector3 _newHandRotation = new Vector3(xRotationHand, 0f, 0f) * _koefGlobal;

                unitSkinSettings.GetRightHandNode().localEulerAngles = -_newHandRotation;
                unitSkinSettings.GetLeftHandNode().localEulerAngles = _newHandRotation;
            }

            if (unitSkinSettings.GetParcelNode())
            {
                float xParcel = amplPosX * Mathf.Cos(_speed * _koefSpeedGlobal * index);
                float yParcel = Mathf.Abs(amplPosY * Mathf.Cos(_speed * _koefSpeedGlobal * index));

                Vector3 _newParcelPosition = (new Vector3(xParcel, yParcel, 0f) * 0.5f * _koefGlobal) + _startParcelPosition;
                Vector3 _newParcelRotation = new Vector3(0f, 0f, -zRotation) * _koefGlobal;

                unitSkinSettings.GetParcelNode().localPosition = _newParcelPosition;
                unitSkinSettings.GetParcelNode().localEulerAngles = _newParcelRotation;
            }
        }
        else
        {
            if (vehicleNodes.Count > 0)
            {
                foreach (VehicleAnimation vehicle in vehicleNodes) vehicle.PlayVehicleAnimation = false;
            }

            _koefGlobal = 1f * _dividerGlobal;
            _koefSpeedGlobal = 1f * _dividerSpeedGlobal;

            if (!IsInShop)
            {
                float _scale = Mathf.Abs(_amplScale * Mathf.Cos(scaleSpeed * index));
                unitSkinSettings.GetBodyAnimationNode().localScale = new Vector3(_scale, _scale, _scale) + Vector3.one;

                if (unitSkinSettings.GetParcelNode())
                {
                    float _scaleParcel = Mathf.Abs(_amplScale * Mathf.Sin(scaleSpeedParcel * index));
                    unitSkinSettings.GetParcelNode().localScale = new Vector3(_scaleParcel, _scaleParcel, _scaleParcel) + Vector3.one;
                }
            }
        }

        if (IsInShop) RotateInShop();

        if (IsWaving) WaveHand();
    }




    private float _shopAmplRotY = 60f;
    private float _shopSpeedRotY = 1f;

    void RotateInShop()
    {
        float yRotation = _shopAmplRotY * Mathf.Cos(_shopSpeedRotY * index);
        unitSkinSettings.GetBodyNode().localEulerAngles = new Vector3(0f, yRotation, 0f);
    }

    float _handStartRotationY = 135f;
    float _handAmplRotZ = 45f;
    float _handSpeedRotZ = 12f;

    void WaveHand()
    {
        float _zRot = _handAmplRotZ * Mathf.Cos(_handSpeedRotZ * index);
        unitSkinSettings.GetRightHandNode().localEulerAngles = new Vector3(0f, 0f, _zRot + _handStartRotationY);
        unitSkinSettings.GetLeftHandNode().localEulerAngles = new Vector3(0f, 0f, -(_zRot + _handStartRotationY));
    }

    public bool IsWaving
    {
        get => _isWaving;
        set
        {
            if (_isWaving == value) return;
            _isWaving = value;
            if (!_isWaving) ResetHand();
        }
    }

    void ResetHand()
    {
        unitSkinSettings.GetRightHandNode().localEulerAngles = Vector3.zero;
        unitSkinSettings.GetLeftHandNode().localEulerAngles = Vector3.zero;
    }

    public bool HasParcel
    {
        get => _hasParcel;
        set
        {
            if (_hasParcel == value) return;
            _hasParcel = value;
        }
    }
    public bool IsWalking
    {
        get => _isWalking;
        set
        {
            if (_isWalking == value) return;
            _isWalking = value;
            if (!_isWalking)
            {
                ResetBodyAnimationNode();
                ResetParcelNode();
            } 
        }
    }

    void ResetBodyAnimationNode()
    {
        unitSkinSettings.GetBodyAnimationNode().localPosition = _startBodyPosition;
        unitSkinSettings.GetBodyAnimationNode().localEulerAngles = _startBodyRotation;
    }

    void ResetParcelNode()
    {
        unitSkinSettings.GetParcelNode().localPosition = _startParcelPosition;
        unitSkinSettings.GetParcelNode().localEulerAngles = Vector3.zero;
    }

    public bool IsAttacking
    {
        get => _isAttacking;
        set
        {
            if (_isAttacking == value) return;
            _isAttacking = value;
        }
    }

    public bool IsInShop
    {
        get => _isInShop;
        set
        {
            if (_isInShop == value) return;
            _isInShop = value;
        }
    }
}
