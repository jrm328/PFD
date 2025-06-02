using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Utilities;
using DG.Tweening;

public class EquipmentShopPopupWindow : PopupWindowBase
{
    public bool hideAdButton = true;
    public GemButton _currencyButton;

    public Camera shopCamera;
    public RawImage rawImage;

    public TextMeshProUGUI unitNameText;
    public TextMeshProUGUI rentInfoText;

    private UnitModel candidatePlayer = new UnitModel();

    [Header("Controllers")]

    public float contentHolderOffset = 250f;

    public float itemOffset = 160f;
    public float itemOffsetZ = 100f;

    public float itemAngleX = 5f;
    public float itemAngleY = -10f;
    public float itemAngleZ = -2f;

    public float snapSpeed = 10f;
    public float scaleOffset = 50f;
    public float scaleSpeed = 1000f;
    public float velocity = 400f;
    public float minScale = 50f;
    public float maxScale = 100f;

    [Header("Other Obects")]
    public GameObject shopSlot;

    public ScrollRect scrollRect;
    private GameObject[] instantiatedItem;
    private Vector2[] itemPos;
    private Vector3[] itemScale;

    public RectTransform contentRect;
    private Vector2 contentPositon;

    public int selectedItemID;
    private bool isScrolling;
    public bool isPartsInstantiated;

    int _partsCount;

    protected override void OnShow()
    {
        base.OnShow();

        SetView();
        SetPositions();

        CurrencyButtonInteraction();
        CurrencyButtonText();

        SetRentInfoText();

        app.model.balanceModel.gemModel.ON_AMOUNT_CHANGE += OnCurrencyChange;

        _currencyButton._button.onClick.AddListener(OnCurrencyButtonClick);

        closeButton.onClick.AddListener(HideWindow);

        InstantiateItems(app.globalConfigs.playerConfigs);
    }

    void SetView()
    {
        RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
        shopCamera.targetTexture = renderTexture;
        rawImage.gameObject.SetActive(true);
        rawImage.texture = renderTexture;
    }
    void SetPositions()
    {
        contentPositon = shopCamera.transform.localPosition - new Vector3(0f, contentHolderOffset, 0f);
        contentRect.anchoredPosition = contentPositon;
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        boxCollider.center = new Vector3(0f, contentPositon.y, 0f);
    }
    private void OnDestroy()
    {
        app.model.balanceModel.gemModel.ON_AMOUNT_CHANGE -= OnCurrencyChange;

        _currencyButton._button.onClick.RemoveListener(OnCurrencyButtonClick);

        closeButton.onClick.RemoveListener(HideWindow);
    }

    void OnCurrencyChange(ulong _prevValue, ulong _value)
    {
        CurrencyButtonInteraction();
        CurrencyButtonText();
    }

    public void CurrencyButtonInteraction()
    {
        if (_currencyButton.currencyModel.CanBuyWithCurrency(GetEquipmentRentPrice())) _currencyButton.SetButtonInteraction(true);
        else _currencyButton.SetButtonInteraction(false);
    }
    public void CurrencyButtonText()
    {
        string _textCost = MathUtil.UlongToString(GetEquipmentRentPrice());
        _currencyButton.SetButtonText(_currencyButton.GetTextSprite(_currencyButton.currencyModel.GetSpriteID()), _textCost);
    }

    ulong GetEquipmentRentPrice()
    {
        return app.model.balanceModel.GetEquipmentRentPrice();
    }

    void OnCurrencyButtonClick()
    {
        app.controller.balanceController.MinusCurrency(_currencyButton.currencyModel, GetEquipmentRentPrice());
        StartBoost();
    }

    private void OnTriggerEnter(Collider other)
    {
        candidatePlayer = other.gameObject.GetComponent<UnitView>().model;
        SetUnitNameText(candidatePlayer.GetID());
    }
    void SetUnitNameText(string _name)
    {
        unitNameText.text = _name;
        unitNameText.color = Color.white;
    }

    void SetRentInfoText()
    {
        rentInfoText.text = "Rent for " + MathUtil.SecondsToMinutes(app.model.balanceModel.GetEquipmentRentDuration());
    }

    void OnAdRewarded(string _itemID)
    {
        if (_itemID == candidatePlayer.GetID())
        {
            StartBoost();
        }
    }

    void StartBoost()
    {
        app.controller.boostsController.StartBoost(candidatePlayer.Config);
        HideWindow();
    }

    public void InstantiateItems(List<UnitConfig> _partsConfigsList)
    {
        isPartsInstantiated = true;

        _partsCount = _partsConfigsList.Count;
        instantiatedItem = new GameObject[_partsCount];
        itemPos = new Vector2[_partsCount];
        itemScale = new Vector3[_partsCount];

        float _position = 0f;

        for (int i = 0; i < _partsCount; i++)
        {
            var model = new UnitModel
            {
                Config = _partsConfigsList[i],
            };

            instantiatedItem[i] = Instantiate(shopSlot, contentRect.transform, false);
            instantiatedItem[i].name = shopSlot.name + "_" + model.GetID();
            var view = instantiatedItem[i].AddComponent<PlayerShopView>();
            view.SetModel(model);
            candidatePlayer = model;
            instantiatedItem[i].transform.localPosition = new Vector3(instantiatedItem[i].transform.localPosition.x + _position, instantiatedItem[i].transform.localPosition.y);
            instantiatedItem[i].transform.localEulerAngles = new Vector3(itemAngleX, itemAngleY, itemAngleZ);
            instantiatedItem[i].transform.localScale = new Vector3(minScale, minScale, minScale);
            itemPos[i] = -instantiatedItem[i].transform.localPosition;
            _position += itemOffset;
        }
    }

    private void FixedUpdate()
    {
        if (!isPartsInstantiated) return;

        if (contentRect.anchoredPosition.x >= itemPos[0].x && !isScrolling || contentRect.anchoredPosition.x <= itemPos[itemPos.Length - 1].x && !isScrolling)
        {
            scrollRect.inertia = false;
        }

        Scale();

        float scrollVelocity = Mathf.Abs(scrollRect.velocity.x);

        if (scrollVelocity < velocity && !isScrolling) scrollRect.inertia = false;

        if (isScrolling || scrollVelocity > velocity) return;

        contentPositon.x = Mathf.SmoothStep(contentRect.anchoredPosition.x, itemPos[selectedItemID].x, snapSpeed * Time.fixedDeltaTime);

        contentRect.anchoredPosition = contentPositon;
    }

    void Scale()
    {
        float nearestPos = float.MaxValue;

        for (int i = 0; i < _partsCount; i++)
        {
            float distance = Mathf.Abs(contentRect.anchoredPosition.x - itemPos[i].x);

            if (distance < nearestPos)
            {
                nearestPos = distance;
                selectedItemID = i;
            }

            float scale = Mathf.Clamp(1 / (distance / itemOffset) * scaleOffset, minScale, maxScale);

            itemScale[i].x = Mathf.SmoothStep(instantiatedItem[i].transform.localScale.x, scale, scaleSpeed * Time.fixedDeltaTime);
            itemScale[i].y = Mathf.SmoothStep(instantiatedItem[i].transform.localScale.y, scale, scaleSpeed * Time.fixedDeltaTime);
            itemScale[i].z = Mathf.SmoothStep(instantiatedItem[i].transform.localScale.z, scale, scaleSpeed * Time.fixedDeltaTime);

            instantiatedItem[i].transform.localScale = itemScale[i];
        }
    }

    public void Scrolling(bool scroll)
    {
        isScrolling = scroll;
        if (scroll) scrollRect.inertia = true;
    }

    protected override void PlayShowAnimation()
    {
        _rect.DOKill();
        float _scale = 0.85f;
        _rect.localScale = new Vector3(_scale, _scale, _scale);
        _rect.anchoredPosition = GetStartPos();
        _rect.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack).OnComplete(() => { });
    }

    protected override void PlayHideAnimation()
    {
        _rect.DOKill();
        float _scale = 0.85f;
        _rect.DOScale(new Vector3(_scale, _scale, _scale), 0.1f)
            .SetEase(Ease.InBack)
            .OnComplete(() =>
            {
                OnHideAnimationComplete();
            });
    }
}
