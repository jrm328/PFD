using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PolicemanView : ReceiverView
{
    public Transform homeTarget;

    protected override void SetTargetOnStart()
    {
        SetTarget(homeTarget);
    }

    protected override void ParcelMatch(bool _bool)
    {
        base.ParcelMatch(_bool);
        if (_bool) SetTarget(app.view.player.transform);
        else SetTarget(homeTarget);
    }

    protected override void PlayAnimation(bool _bool)
    {
        unitAnimation.IsWalking = _bool;
    }

    protected override void Update()
    {
        base.Update();
        Move();
    }

    private void Move()
    {
        if (Target != null)
        {
            float _dist = Vector3.Distance(Target.position, transform.position);
            if (_dist > 2f)
            {
                transform.position += transform.forward * model.GetSpeed() * Time.deltaTime;
            }
        }
    }

}

