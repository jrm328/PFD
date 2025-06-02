using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkateboardAnimation : VehicleAnimation
{
    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private float amplPosX = 0.5f;
    [SerializeField]
    private float amplRotY = 15f;

    void Start()
    {
        _startPos = transform.localPosition;
    }

    void Update()
    {
        _index += Time.deltaTime;

        if (!PlayVehicleAnimation) return;

        float _xPos = amplPosX * Mathf.Cos(speed * _index);
        float _yRot = amplRotY * Mathf.Sin(speed * _index);

        Vector3 _newPos = (new Vector3(_xPos, 0f, 0f)) + _startPos;
        Vector3 _newRot = new Vector3(0f, -_yRot, 0f);

        transform.localPosition = _newPos;
        transform.localEulerAngles = _newRot;
    }
}
