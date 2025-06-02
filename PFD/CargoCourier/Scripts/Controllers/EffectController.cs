using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    //public enum effectsEnum {None, Meet, CatridgesRifle, CatridgesShotgun, Money }
    //public effectsEnum EffectEnum;
    private GameObject helper;

    //EffectConfig effectConfig;
    //Effects.EffectSpawnParts _effect;
    //private CurrencyConfig config;

    void Start()
    {
        //switch (EffectEnum)
        //{
        //    case effectsEnum.Meet:
        //        _effect = Effects.instance.Meet;
        //        break;
        //    case effectsEnum.CatridgesRifle:
        //        _effect = Effects.instance.CatridgesRifle;
        //        break;
        //    case effectsEnum.CatridgesShotgun:
        //        _effect = Effects.instance.CatridgesShotgun;
        //        break;
        //    case effectsEnum.Money:
        //        _effect = Effects.instance.Money;
        //        break;
        //}

        helper = new GameObject();
        helper.transform.parent = transform;
        helper.transform.localPosition = Vector3.zero;
        helper.transform.localRotation = Quaternion.identity;
        helper.name = "helper_" + name;
    }

    public void FireEffect(CurrencyConfig _config, int _pieces, Vector3 _effectPosition, Transform _objectToLookAt)
    {
        StartCoroutine(CoroutineInstantiateEffect(_config, _pieces, _effectPosition, _objectToLookAt));
    }

    private IEnumerator CoroutineInstantiateEffect(CurrencyConfig _config, int _pieces, Vector3 _effectPosition, Transform _objectToLookAt)
    {
        helper.transform.position = _effectPosition;
        helper.transform.LookAt(_objectToLookAt);
        helper.transform.eulerAngles = new Vector3(0f, helper.transform.eulerAngles.y, 0f);

        float _time = 0f;
        for (int i = 0; i < _pieces; i++)
        {
            yield return new WaitForSeconds(_time);
            SpawnPiece(_config);
            _time = 0.2f;
        }
    }

    void SpawnPiece(CurrencyConfig _config)
    {
        //float scale = Random.Range(_effect.scaleMin, _effect.scaleMax);
        GameObject meetPart = Instantiate(_config.skin, helper.transform.position, Quaternion.identity);
        meetPart.transform.eulerAngles = new Vector3(GetRandomAngle(), GetRandomAngle(), GetRandomAngle());
        //float scale = Random.Range(0.5f, 1f);
        //meetPart.transform.localScale = new Vector3(scale, scale, scale);

        Rigidbody rb = meetPart.GetComponent<Rigidbody>();
        //float _mass = 1f;
        //rb.mass = _mass;

        rb.linearDamping = 0f;
        rb.useGravity = true;

        float _force = 10f;
        float _right = Random.Range(-0.2f, 0.2f);
        float _up = Random.Range(0.6f, 1f);
        float _forward = Random.Range(0.5f, 1f);

        Vector3 force = helper.transform.right * _right * _force
         + helper.transform.up * _up * _force
        + helper.transform.forward * _forward * _force;

        rb.AddForceAtPosition(force, transform.position, ForceMode.Impulse);

        Destroy(meetPart, 5f);
    }

    float GetRandomAngle()
    {
        return Random.Range(0f, 360f);
    }
}
