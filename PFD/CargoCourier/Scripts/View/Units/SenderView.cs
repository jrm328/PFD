using UnityEngine;
using System.Collections;
using System;

public class SenderView : UnitView
{
    public bool spawnParcelOnStart;

    private void Start()
    {
        GetParcelSpawnRate();
        SetModel(model);

        if(spawnParcelOnStart) UpdateParcelSkin(parcelModel);
    }

    private void GetParcelSpawnRate()
    {
        _parcelSpawnRate = app.model.balanceModel.parcelSpawnRate;
    }

    void LookAtPlayer(Transform player)
    {
        transform.LookAt(player);
    }

    public float _timeFromLastSpawn;
    float _parcelSpawnRate;

    void Update()
    {
        LookAtPlayer(app.view.player.transform);

        if (!skinParcel)
        {
            _timeFromLastSpawn += Time.deltaTime;
            if (_timeFromLastSpawn > _parcelSpawnRate)
            {
                _timeFromLastSpawn = 0;
                UpdateParcelSkin(parcelModel);
            }
        }
    }
}
