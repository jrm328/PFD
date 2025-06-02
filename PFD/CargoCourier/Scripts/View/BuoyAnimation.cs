using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuoyAnimation : MonoBehaviour
{
    //public float[] speedsPos;
    //public float[] speedsRot;

    private float indexPos;
    private float indexRot;

    public float _amplPosY;
    public float _speedPosY;

    public float _amplRotZ;
    public float _speedRotZ;

    private Vector3 _startPosition;

    void Start()
    {
        _startPosition = transform.localPosition;

        indexPos = Random.Range(0f, 1f);
        indexRot = Random.Range(0f, 1f);
    }

    void Update()
    {
        indexPos += Time.deltaTime;
        indexRot += Time.deltaTime;

        float _yPos = _amplPosY * Mathf.Cos(_speedPosY * indexPos);
        Vector3 _newPos = new Vector3(0f, _yPos, 0f) + _startPosition;
        transform.localPosition = _newPos;

        float _zRot = _amplRotZ * Mathf.Cos(_speedRotZ * indexRot);
        Vector3 _newRotation = new Vector3(0f, 0f, _zRot);
        transform.localEulerAngles = _newRotation;
    }
}
