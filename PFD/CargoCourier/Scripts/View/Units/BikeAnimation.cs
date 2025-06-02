using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeAnimation : VehicleAnimation
{
    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private float amplPosX = 0.5f;
    [SerializeField]
    private float amplPosY = 0.2f;
    [SerializeField]
    private float amplRotZ = 30f;
    [SerializeField]
    private float amplRotY = 30f;
    [SerializeField]
    private Transform[] wheels;
    [SerializeField]
    private float wheelsAngle = 5f;


    void Start()
    {
        _startPos = transform.localPosition;
    }

    void Update()
    {
        _index += Time.deltaTime;

        if (!PlayVehicleAnimation) return;

        float _xPos;
        float _zRot;
        float _yRot;

        _yRot = amplRotY * Mathf.Sin(speed * _index);
        _xPos = amplPosX * Mathf.Cos(speed * _index);
        _zRot = amplRotZ * Mathf.Cos(speed * _index);

        float _yPos = Mathf.Abs(amplPosY * Mathf.Sin(speed * _index));

        Vector3 _newPos = (new Vector3(_xPos, _yPos, 0f)) + _startPos;
        Vector3 _newRot = new Vector3(0f, _yRot, _zRot);

        transform.localPosition = _newPos;
        transform.localEulerAngles = _newRot;

        foreach (Transform _wheel in wheels)
        {
            _wheel.Rotate(wheelsAngle, 0f, 0f, Space.Self);
        }
    }

}
