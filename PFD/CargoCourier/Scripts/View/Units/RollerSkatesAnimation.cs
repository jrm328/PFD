using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerSkatesAnimation : VehicleAnimation
{
    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private float speedRot = 10f;
    [SerializeField]
    private float amplPosX = 0.5f;
    [SerializeField]
    private float amplPosY = 0.2f;
    [SerializeField]
    private float amplRotZ = 30f;
    [SerializeField]
    private float amplRotY = 30f;

    public bool _timeShift;
    public float _timeShiftAmount = 0.1f;

    void Start()
    {
        _startPos = transform.localPosition;
    }

    void Update()
    {
        _index += Time.deltaTime;

        if (!_timeShift) _timeShiftAmount = 0f;

        if (!PlayVehicleAnimation) return;

        float _xPos;
        float _zRot;
        float _yRot;

        _yRot = amplRotY * Mathf.Cos(speedRot * (_index + _timeShiftAmount));
        _xPos = amplPosX * Mathf.Cos(speed * (_index + _timeShiftAmount));
        _zRot = amplRotZ * Mathf.Cos(speed * (_index + _timeShiftAmount));

        float _yPos = Mathf.Abs(amplPosY * Mathf.Sin(speed * (_index + _timeShiftAmount)));

        Vector3 _newPos = (new Vector3(-_xPos, _yPos, 0f)) + _startPos;
        Vector3 _newRot = new Vector3(0f, _yRot, -_zRot);

        transform.localPosition = _newPos;
        transform.localEulerAngles = _newRot;
    }
}
