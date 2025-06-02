using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeAnimation : MonoBehaviour
{
    float index, indexX, indexY, indexZ;

    public float[] amplitudes;
    float _amplitudeX, _amplitudeY, _amplitudeZ;

    public float[] rotSpeeds;
    float _rotSpeedX, _rotSpeedY, _rotSpeedZ;

    void Start()
    {
        _amplitudeX = amplitudes[Random.Range(0, amplitudes.Length)];
        _amplitudeY = amplitudes[Random.Range(0, amplitudes.Length)];
        _amplitudeZ = amplitudes[Random.Range(0, amplitudes.Length)];

        _rotSpeedX = rotSpeeds[Random.Range(0, rotSpeeds.Length)];
        _rotSpeedY = rotSpeeds[Random.Range(0, rotSpeeds.Length)];
        _rotSpeedZ = rotSpeeds[Random.Range(0, rotSpeeds.Length)];
    }

    void Update()
    {
        index += Time.deltaTime;

        float xRot = _amplitudeX * Mathf.Cos(_rotSpeedX * index);
        float yRot = _amplitudeY * Mathf.Cos(_rotSpeedY * index);
        float zRot = _amplitudeZ * Mathf.Cos(_rotSpeedZ * index);

        Vector3 _newBodyRotation = new Vector3(xRot / 2f, yRot, zRot);
        transform.localEulerAngles = _newBodyRotation;
    }
}
