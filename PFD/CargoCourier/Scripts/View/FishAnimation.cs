using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAnimation : MonoBehaviour
{
    public Transform body;
    public Transform tail;
    public float[] rotSpeeds;

    private float index;
    private float _amplitudeY;
    private float _rotSpeedY;

    void Start()
    {
        _amplitudeY = 45f;
        _rotSpeedY = rotSpeeds[Random.Range(0, rotSpeeds.Length)];
    }

    void Update()
    {
        index += Time.deltaTime;

        float yRot = _amplitudeY * Mathf.Cos(_rotSpeedY * index);

        Vector3 _newRotation = new Vector3(0f, yRot, 0f);
        tail.localEulerAngles = _newRotation;
        body.localEulerAngles = -_newRotation * 0.1f;
    }
}
