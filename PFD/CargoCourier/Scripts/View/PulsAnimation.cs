using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulsAnimation : MonoBehaviour
{
    [SerializeField]
    float amplScale = 0.2f;
    [SerializeField]
    float speed = 5f;
    [SerializeField]
    bool _isPulsDown;

    float index;
    float _scale;

    private void Update()
    {
        PlayPulsAnimation(amplScale, speed, transform);
    }

    void PlayPulsAnimation(float _amplScale, float _speed, Transform _object)
    {
        index += Time.deltaTime;
        _scale = Mathf.Abs(_amplScale * Mathf.Cos(_speed * index));
        if (_isPulsDown) _scale = _scale - amplScale;
        _object.localScale = new Vector3(_scale, _scale, _scale) + Vector3.one;
    }
}
