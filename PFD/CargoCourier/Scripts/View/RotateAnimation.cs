using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAnimation : MonoBehaviour
{
    [SerializeField]
    private float _speedRotY = 0.5f;
    [SerializeField]
    private float _amplRotY = 60f;
    float index = 45f;


    void Update()
    {
        index += Time.deltaTime;

        RotateInShop();
    }

    void RotateInShop()
    {
        float yRotation = _amplRotY * Mathf.Cos(_speedRotY * index);
        transform.localEulerAngles = new Vector3(0f, yRotation, 0f);
    }
}
