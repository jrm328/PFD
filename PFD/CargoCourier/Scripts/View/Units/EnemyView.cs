using UnityEngine;
using System.Collections;
using System;

public class EnemyView : UnitView
{
    private void Start()
    {
        app.controller.ON_PLAYER_GET_PARCEL += OnPlayerGetParcel;
        app.controller.ON_PLAYER_LOST_PARCEL += OnPlayerLostParcel;

        SetWalkingAnimation(true);
        SetPosition();
        SetTargetPlayer();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Trap"))
        {
            model.Health = 0f;
        }
    }

    protected override void AddRigidBody()
    {
        base.AddRigidBody();
    }

    protected override void OnDied(UnitModel unitModel)
    {
        Destroy(gameObject, 2f);
    }

    void SetPosition()
    {
        transform.position = app.view.GetEnemySpawnPoint().position;
    }

    Transform _target;
    Transform Target
    {
        get => _target;
        set
        {
            if (_target == value) return;
            _target = value;
        }
    }

    public void OnPlayerGetParcel(ParcelConfig _parcel)
    {
        if (parcelModel.GetID() == _parcel.ID) SetTargetPlayer();
    }

    public void OnPlayerLostParcel(ParcelConfig _parcel)
    {
        SetTargetOther();
    }

    void SetTargetPlayer()
    {
        Target = app.view.player.transform;
    }

    void SetTargetOther()
    {
        Target = app.view.GetEnemySpawnPoint();
    }

    private void Update()
    {
        if (!model.GetIsDead() && !model.GetIsAttacking())
        {
            if (Target != null)
            {
                float _dist = Vector3.Distance(Target.position, transform.position);
                if (_dist > 1f)
                {
                    transform.position += transform.forward * model.GetSpeed() * Time.deltaTime;

                    var rotation = Quaternion.LookRotation(Target.position - transform.position);
                    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * model.GetRotationSpeed());
                }
            }
        }
    }

}
