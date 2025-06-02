using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfinityTrajectory : MonoBehaviour
{
    public float speed = 1f;
    public float koef = 1f;
    private Vector2 _startPos;

    void Start()
    {
        _startPos = transform.localPosition;
    }

    void Update()
    {
        float t = Time.time;

        float scaleX = koef / (3 - Mathf.Cos(2 * (t * speed)));
        float x = scaleX * Mathf.Cos(t * speed);
        float y = scaleX * Mathf.Sin(2 * (t * speed)) / 2;

        transform.localPosition = _startPos + new Vector2(x, y);
    }
}
