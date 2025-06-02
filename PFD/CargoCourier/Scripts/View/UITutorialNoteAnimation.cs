using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITutorialNoteAnimation : MonoBehaviour
{
    private float amplPosX;
    private float amplPosY;
    private float amplPosZ;

    private float speed;
    private float index;

    private float scaleAmplitude;

    Vector2 _startBodyPosition;

    void Start()
    {
        amplPosX = 0f;
        amplPosY = 15f;
        amplPosZ = 0f;

        speed = 4f;

        scaleAmplitude = 0.1f;

        _startBodyPosition = transform.localPosition;
    }

    void Update()
    {
        index += Time.deltaTime;

        float x = amplPosX * Mathf.Cos(speed * index);
        float y = Mathf.Abs(amplPosY * Mathf.Sin(speed * index));
        float z = amplPosZ * Mathf.Cos(speed * index);

        Vector2 _newBodyPosition = new Vector2(x, y) + _startBodyPosition;
        transform.localPosition = _newBodyPosition;

        float _scale = Mathf.Abs(scaleAmplitude * Mathf.Sin(speed * index));
        transform.localScale = new Vector2(_scale, _scale) + Vector2.one;
    }
}
