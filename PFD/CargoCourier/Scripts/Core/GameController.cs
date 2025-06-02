using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : GameElement
{
    public event Action<bool> ON_TUTORIAL_CHANGED;
    public event Action ON_TUTORIAL_FINISHED;
    public event Action<ParcelConfig> ON_PLAYER_GET_PARCEL;
    public event Action<ParcelConfig> ON_PLAYER_LOST_PARCEL;
    public event Action<ParcelModel, Vector3> ON_PARCEL_DELIVERED;
    public event Action<CurrencyModel> ON_CURRENCY_DESTROYED;

    public TutorialController tutorialController;
    public BalanceController balanceController;
    public BoostsController boostsController;
    public GroundController groundController;
    public PopupWindowController popupWindowController;
    
    private void Awake()
    {
        GetIsTutorial();

        CreateAllCityModels();
        CreateDefaultCityModel();
        LoadCurrentCity();
        SetCityBuildingsVisibility(app.model.currentCityModel);

        balanceController.LoadBalance();
        app.view.CreatePlayerView();
    }

    private void Start()
    {
        app.model.currentCityModel.ON_CONFIG_CHANGED += SetCityBuildingsVisibility;

        app.view.gameHud.openShopButton.onClick.AddListener(OpenShopPopup);

        CheckIfLiftUI();
    }

    public void GetIsTutorial()
    {
        if (app.gameSettings.showTutorial)
        {
            if (PlayerPrefs.GetInt(PlayerPrefsKeys.tutorialStatus, 1) == 1) IsTutorial = true;
            else IsTutorial = false;
        }
        else
        {
            IsTutorial = false;
            }
        tutorialController.TutorialCanvasVisibility(IsTutorial);
        tutorialController.enabled = IsTutorial;
    }

    public void FinishTutorial()
    {
        Debug.Log("GameController. ON_TUTORIAL_FINISHED.");

        IsTutorial = false;
        tutorialController.enabled = false;
        PlayerPrefs.SetInt(PlayerPrefsKeys.tutorialStatus, 0);

        ON_TUTORIAL_FINISHED?.Invoke();
    }

    public bool _isTutorial;
    public bool IsTutorial
    {
        get => _isTutorial;
        set
        {
            if (_isTutorial == value) return;
            _isTutorial = value;
            ON_TUTORIAL_CHANGED?.Invoke(_isTutorial);
        }
    }



    void CreateAllCityModels()
    {
        app.model.cityModels.Clear();
        for (int i = 0; i < app.globalConfigs.cityConfigs.Count; i++)
        {
            CityModel _cityModel = new CityModel
            {
                Config = app.globalConfigs.cityConfigs[i]
                };
            app.model.cityModels.Add(_cityModel);
        }
    }
    void CreateDefaultCityModel()
    {
        CityModel _cityModel = new CityModel
        {
            Config = app.globalConfigs.app.globalConfigs.defaultCityConfig
        };
        app.model.defaultCityModel = _cityModel;
    }
    void LoadCurrentCity()
    {
        string _defaultCityID = app.model.defaultCityModel.GetID();
        string _currentCityID = PlayerPrefs.GetString(PlayerPrefsKeys.currentCityKey, _defaultCityID);
        foreach (CityModel _cityModel in app.model.cityModels)
        {
            if (_currentCityID == _cityModel.GetID())
            {
                if(_cityModel.GetIsComingSoon()) app.model.currentCityModel.Config = app.model.defaultCityModel.Config;
                else app.model.currentCityModel.Config = _cityModel.Config;
                break;
            }
        }
    }
    void SetCityBuildingsVisibility(CityModel _currentCityModel)
    {
        foreach (CityView _cityView in app.view.cityViews)
        {
            if (_cityView.model.GetID() != _currentCityModel.GetID()) _cityView.gameObject.SetActive(false);
            else _cityView.gameObject.SetActive(true);
        }
    }
    public void ChangeCurrentCity(CityModel _cityModel)
    {
        PlayerPrefs.SetString(PlayerPrefsKeys.currentCityKey, _cityModel.GetID());
        app.model.currentCityModel.Config = _cityModel.Config;
    }

    public void LiftTopScreenElements()
    {
        Debug.Log("GameController. LiftUI");
        app.view.gameHud.topScreenElements.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }

    public void OnCurrencyDestroyedFireEvent(CurrencyModel currencyModel)
    {
        Debug.Log("GameController. OnCurrencyDestroyedFireEvent");
        ON_CURRENCY_DESTROYED?.Invoke(currencyModel);
    }

    public void OnPlayerGetParcelFireEvent(ParcelConfig parcelConfig)
    {
        Debug.Log("GameController. OnPlayerGetParcelFireEvent");
        ON_PLAYER_GET_PARCEL?.Invoke(parcelConfig);
    }
    public void OnPlayerLostParcelFireEvent(ParcelConfig parcelConfig)
    {
        Debug.Log("GameController. OnPlayerLostParcelFireEvent");
        ON_PLAYER_LOST_PARCEL?.Invoke(parcelConfig);
    }

    public void OnParcelDeliveredFireEvent(ParcelModel parcel, Vector3 _effectPosition)
    {
        Debug.Log("GameController. OnParcelDeliveredFireEvent");
        ON_PARCEL_DELIVERED?.Invoke(parcel, _effectPosition);
        FinishTutorial();

        FireEffect(app.model.balanceModel.cashModel.Config, parcel.GetCost(), _effectPosition, app.view.player.transform);

        if(app.controller.balanceController.ReachedGemMilestone())
            FireEffect(app.model.balanceModel.gemModel.Config, 1, _effectPosition, app.view.player.transform);
    }



    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void FireEffect(CurrencyConfig _config, int _pieces, Vector3 _effectPosition, Transform _objectToLookAt)
    {
        GetComponent<EffectController>().FireEffect(_config, _pieces, _effectPosition + Vector3.up * 3f, _objectToLookAt);
    }


    void CheckIfLiftUI()
    {
        float screenRatio = (1f * Screen.height) / (1f * Screen.width);
        if (screenRatio < 1.8f)
        {
            //Debug.Log("16:9 iPhones - models 5, SE, up to 8+");
            LiftTopScreenElements();
        }
        //else Debug.Log("19.5:9 iPhones - models X, Xs,  Xr, Xsmax");
    }

    private float _time;
    private void Update()
    {
        if (app.gameSettings.showCheatPanel)
        {
#if UNITY_EDITOR
            if (Input.GetMouseButton(0))
#else
            if (Input.touchCount == 3)
#endif
            {
                _time += Time.deltaTime;
                if (_time >= 3f)
                {
                    FireCheatPopup();
                    _time = 0f;
                }
            }
            else _time = 0f;
        }
    }

    void FireCheatPopup()
    {
        popupWindowController.ShowWindow("cheat_popup");
    }

    void OpenShopPopup()
    {
        popupWindowController.ShowWindow("shop_popup");
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            //app.controller.saveSystemController.SaveData();
        }
    }
}