using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using UnityEngine.Events;

public class BuildingView : GameElement
{
    public GameObject skin;
    public GameObject skinLocked;
    public BuildingModel model;

    private void Start()
    {
        model.ON_BECKOME_UNLOCKED += OnBeckomeUnlocked;
        app.controller.ON_TUTORIAL_CHANGED += OnTutorialChanged;

        AddRigidBody();
        SetBuilding();
    }

    void OnTutorialChanged(bool _isTutorial)
    {
        SetBuilding();
    }

    protected virtual void SetBuilding()
    {
        if (app.controller.IsTutorial)
        {
            if (skin) skin.SetActive(false);
            if (skinLocked) skinLocked.SetActive(false);
        }
        else
        {
            if (model.Config == null) Debug.LogError("BuildingView. " + name + " Has No Config.");
            else
            {
                skin.SetActive(model.GetIsAvailable());
                skin.GetComponent<DoAnimations>().DoScaleOutAnimation();

                if (skinLocked) skinLocked.SetActive(!model.GetIsAvailable());
                if (skinLocked) skinLocked.GetComponent<DoAnimations>().DoScaleOutAnimation();
            }
        }
    }

    private void OnDestroy()
    {
        model.ON_BECKOME_UNLOCKED -= OnBeckomeUnlocked;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnPlayerEnterBuilding();
        }
    }

    protected virtual void OnPlayerEnterBuilding()
    {
        if (!model.GetIsAvailable())
        {
            skinLocked.GetComponent<DoAnimations>().DoScaleInAnimation();
            skinLocked.GetComponent<DoAnimations>().ON_SCALE_IN_ANIMATION_FINISHED += DoScaleOutWhenBuildingLocked;
        }
    }

    void DoScaleOutWhenBuildingLocked()
    {
        skinLocked.GetComponent<DoAnimations>().ON_SCALE_IN_ANIMATION_FINISHED -= DoScaleOutWhenBuildingLocked;
        skinLocked.GetComponent<DoAnimations>().DoScaleOutAnimation();
        FirePopup();
    }

    protected virtual void FirePopup()
    {
        app.controller.popupWindowController.ShowBuildingLockedPopup(model);
    }

    void OnBeckomeUnlocked()
    {
        skinLocked.GetComponent<DoAnimations>().DoScaleInAnimation();
        skinLocked.GetComponent<DoAnimations>().ON_SCALE_IN_ANIMATION_FINISHED += DoScaleOutWhenBuildingUnlocked;
    }

    void DoScaleOutWhenBuildingUnlocked()
    {
        skinLocked.SetActive(false);
        skin.SetActive(true);
        skin.GetComponent<DoAnimations>().DoScaleOutAnimation();
    }

    protected virtual void GetSkinSettings()
    {

    }

    protected virtual void AddCollider()
    {

    }

    protected virtual void AddRigidBody()
    {
        var rb = gameObject.AddComponent<Rigidbody>();
        rb.mass = 50;
        rb.linearDamping = Mathf.Infinity;
        rb.angularDamping = Mathf.Infinity;
        rb.useGravity = false;
        rb.isKinematic = true;
    }
}
