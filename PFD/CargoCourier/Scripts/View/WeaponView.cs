using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using UnityEngine.Events;
//using Core.PopupWindows.Impl;
//using Core.PopupWindows;
//using Utilities;

public class WeaponView: GameElement
{
    public event Action<Transform> ON_WEAPON_CREATED;

    public GameObject skin;
    public WeaponModel model;
    private WeaponSkinSettings weaponSkinSettings;

    private float _rateOfFire;

    private void OnEnable()
    {
        app.view.GetPlayerView().ON_CAN_ATTACK += OnCanAttack;
    }

    private void OnDestroy()
    {
        app.view.GetPlayerView().ON_CAN_ATTACK -= OnCanAttack;
    }

    void OnCanAttack()
    {
        if (_rateOfFire >= model.GetRateOfFire())
        {
            _rateOfFire = 0;
            app.view.GetPlayerView().SetAttacking(true);
            StartCoroutine(CoroutineShooting());
        }
        else app.view.GetPlayerView().SetAttacking(false);
    }

    private void Update()
    {
        _rateOfFire += Time.deltaTime;
    }

    public void SetModel(WeaponModel _model, Transform nodeForWeapon)
    {
        model = _model;
        //Subscribe();

        Parent(nodeForWeapon);
        ResetTransform();

        UpdateView(model);
    }

    private void Parent(Transform nodeForWeapon)
    {
        transform.parent = nodeForWeapon;
    }

    private void ResetTransform()
    {
        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.one;
        transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
    }

    private void UpdateView(WeaponModel _model)
    {
        Debug.Log("WeaponView. UpdateView: " + _model.Config.ID);
        if (_model.Config != null)
        {
            if (skin != null) Destroy(skin.gameObject);
            skin = Instantiate(model.Config.skin, transform);

            GetSkinSettings();
        }
    }

    void GetSkinSettings()
    {
        if (skin != null)
        {
            weaponSkinSettings = skin.GetComponent<WeaponSkinSettings>();
        }
    }

    IEnumerator CoroutineShooting()
    {
        yield return new WaitForEndOfFrame();

        float _ang;
        float _accuracyAngle = model.GetAccuracyAngle();
        float _accuracyPositionX = model.GetAccuracyPositionX();

        _ang = Random.Range(-model.GetAccuracyAngle(), model.GetAccuracyAngle());

        for (int i = 0; i < weaponSkinSettings.bulletNodes.Length; i++)
        {
            float bulletScale = Random.Range(model.GetBulletScaleMin(), model.GetBulletScaleMax());
            GameObject bulletOut = Instantiate(model.GetBullet(), transform.position, Quaternion.identity);
            bulletOut.transform.localScale = new Vector3(bulletScale, bulletScale, bulletScale);
            bulletOut.transform.localRotation = Quaternion.Euler
            (0f, transform.eulerAngles.y + _ang, 0f);

            Vector3 bulletNodePosition = weaponSkinSettings.bulletNodes[i].position;

            bulletOut.transform.position = new Vector3(bulletNodePosition.x + Random.Range(-_accuracyPositionX, _accuracyPositionX),
bulletNodePosition.y, bulletNodePosition.z);

            Color _color = Color.HSVToRGB(Random.Range(0f, 80f / 360f), Random.Range(0f, 0.5f), Random.Range(0.6f, 1f));
            bulletOut.GetComponentInChildren<Renderer>().material.color = _color;

            //AudioManager.instance.PlayWeaponSound(_weapon.audioClips);

            //if (catridgeEffectControllerScript != null)
            //{
            //    StartCoroutine(catridgeEffectControllerScript.CoroutineInstantiateEffect(1, weaponCatridgePos.position, null));
            //}
            //Destroy(bulletOut, _weapon.bulletLifeTime);
            Destroy(bulletOut, 2f);

            _ang = _ang * -1f;
        }
        yield return null;
    }





}