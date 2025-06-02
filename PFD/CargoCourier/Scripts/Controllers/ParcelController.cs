using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParcelController : GameElement
{
    protected float Animation;
    Vector3 _startPosition;
    private float _flightTime;
    private float _height;
    private float _rotSpeed;
    private int _rotDirection;
    private Transform _parcelDestination;
    private ParcelModel _parcel;
    private UnitView _receiver;

    void Start()
    {
        _flightTime = 0.7f;
        _height = 4f;
        _rotSpeed = 750f;
        _rotDirection = GetDirection();
    }

    public float GetFlightTime()
    {
        return _flightTime;
    }

    public void FireParcel(ParcelConfig parcelConfig, Transform parcelDestination, UnitView receiver)
    {
        _parcel = new ParcelModel
        {
            Config = parcelConfig
        };
        _startPosition = transform.position;
        _parcelDestination = parcelDestination;
        _receiver = receiver;
    }

    int GetDirection()
    {
        return Random.Range(0, 2) * 2 - 1;
    }

    void Update()
    {
        if(_parcelDestination != null)
        {
            Animation += Time.deltaTime;
            if (Animation > _flightTime)
            {
                Destroy(gameObject);
                if (!_receiver) app.controller.OnPlayerGetParcelFireEvent(_parcel.Config);
                else app.controller.OnParcelDeliveredFireEvent(_parcel, _receiver.transform.position);
            }

            Animation = Animation % _flightTime;
            transform.position = MathParabola.Parabola(_startPosition, _parcelDestination.position, _height, Animation / _flightTime);
            transform.Rotate(Vector3.up * _rotSpeed * _rotDirection * Time.deltaTime, Space.Self);
        }
    }
}
