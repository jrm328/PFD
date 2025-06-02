using System.Collections.Generic;
using UnityEngine;

public class GameView: GameElement
{
    public GameHud gameHud;
    public BoostsHud boostsHud;
    public TutorialHud tutorialHud;

    public GameObject player;
    //private GameObject _weapon;
    //private GameObject _vehicle;

    public WeaponModel weaponModel = new WeaponModel();
    public VehicleModel vehicleModel = new VehicleModel();

    private PlayerView playerView;
    public List<CityView> cityViews;

    public List<Transform> enemySpawnPoints;

    public Transform GetEnemySpawnPoint()
    {
        return enemySpawnPoints[Random.Range(0, enemySpawnPoints.Count)];
    }

    private void Start()
    {
        app.controller.ON_PLAYER_GET_PARCEL += OnPlayerGetParcel;
    }



    public void CreatePlayerView()
    {
        Debug.Log("GameView. CreatePlayerView.");
        playerView = player.GetComponent<PlayerView>();
        //playerView.ON_NODE_FOR_WEAPON_CREATED += CreateWeaponView;
        //playerView.ON_NODE_FOR_VEHICLE_CREATED += CreateVehicleView;
        UnitModel playerModel = new UnitModel
        {
            Config = app.globalConfigs.defaultPlayerConfig
        };
        playerView.SetModel(playerModel);
    }

    //public void CreateWeaponView(Transform nodeForWeapon)
    //{
    //    Debug.Log("GameView. CreateWeaponView.");
    //    weaponModel.Config = app.globalConfigs.weaponConfigs[Random.Range(0, app.globalConfigs.weaponConfigs.Count)];
    //    _weapon = new GameObject();
    //    WeaponView weaponView = _weapon.AddComponent<WeaponView>();
    //    weaponView.SetModel(weaponModel, nodeForWeapon);
    //}

    //public void CreateVehicleView(Transform nodeForWeapon)
    //{
    //    Debug.Log("GameView. CreateVehicleView.");
    //    vehicleModel.Config = app.globalConfigs.vehicleConfigs[Random.Range(0, app.globalConfigs.vehicleConfigs.Count)];
    //    _vehicle = new GameObject();
    //    VehicleView vehicleView = _vehicle.AddComponent<VehicleView>();
    //    vehicleView.SetModel(vehicleModel, nodeForWeapon);
    //}

    //public WeaponView GetWeaponView()
    //{
    //    return _weapon.GetComponent<WeaponView>();
    //}

    public PlayerView GetPlayerView()
    {
        return playerView;
    }

    void OnPlayerGetParcel(ParcelConfig _parcelConfig)
    {
        if (app.gameSettings.spawnEnemies && !app.controller.IsTutorial) CreateEnemyView(_parcelConfig);
    }

    public void CreateEnemyView(ParcelConfig _parcelConfig)
    {
        //if ()
        {
            Debug.Log("GameView. CreateEnemyView.");
            UnitModel enemyModel = new UnitModel
            {
                //Config = _parcel.GetEnemyConfig()
                Config = app.globalConfigs.enemyConfigs[0]
            };
            if (enemyModel.Config == null)
            {
                Debug.LogError("GameView. Warning. Enemy Config has not been set in Parcel's Config. Parcel ID: " + _parcelConfig.ID);
                return;
            }
            GameObject enemy = new GameObject
            {
                name = enemyModel.GetID()
            };
            EnemyView enemyView = enemy.AddComponent<EnemyView>();
            enemyView.SetModel(enemyModel);
            enemyView.parcelModel.Config = _parcelConfig;
        }
    }
}