using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleAnimation : MonoBehaviour
{
    [HideInInspector]
    public Vector3 _startPos;
    [HideInInspector]
    public float _index;

    bool _playVehicleAnimation;
    public bool PlayVehicleAnimation
    {
        get => _playVehicleAnimation;
        set
        {
            if (_playVehicleAnimation == value) return;
            _playVehicleAnimation = value;
            if (!_playVehicleAnimation) ResetNode();
        }
    }

    void ResetNode()
    {
        transform.localEulerAngles = Vector3.zero;
        transform.localPosition = _startPos;
    }
}
