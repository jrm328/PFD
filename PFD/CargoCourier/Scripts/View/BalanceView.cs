using System.Collections;
using TMPro;
using UnityEngine;
using Utilities;

public class BalanceView : GameElement
{
    public BalanceModel _balanceModel
    {
        get => app.model.balanceModel;
    }

    [SerializeField]
    private TextMeshProUGUI _cashIconText;
    [SerializeField]
    private TextMeshProUGUI _cashText;
    [SerializeField]
    private TextMeshProUGUI _gemIconText;
    [SerializeField]
    private TextMeshProUGUI _gemText;
    [SerializeField]
    private TextMeshProUGUI _parcelsDeliveredIconText;
    [SerializeField]
    private TextMeshProUGUI _parcelsDeliveredText;


    private void Awake()
    {
        _balanceModel.cashModel.ON_AMOUNT_CHANGE += OnCashChange;
        _balanceModel.gemModel.ON_AMOUNT_CHANGE += OnGemChange;
        _balanceModel.parcelsModel.ON_AMOUNT_CHANGE += OnParcelsDeliveredChange;
    }

    void Start()
    {
        SetText(_cashText, _cashIconText, _balanceModel.cashModel.Amount);
        SetText(_gemText, _gemIconText, _balanceModel.gemModel.Amount);
        SetText(_parcelsDeliveredText, _parcelsDeliveredIconText, _balanceModel.parcelsModel.Amount);

        SetIcon(_cashIconText, _balanceModel.cashModel.GetSpriteID());
        SetIcon(_gemIconText, _balanceModel.gemModel.GetSpriteID());
        SetIcon(_parcelsDeliveredIconText, _balanceModel.parcelsModel.GetSpriteID());
    }

    private void OnDestroy()
    {
        _balanceModel.cashModel.ON_AMOUNT_CHANGE -= OnCashChange;
        _balanceModel.gemModel.ON_AMOUNT_CHANGE -= OnGemChange;
        _balanceModel.parcelsModel.ON_AMOUNT_CHANGE -= OnParcelsDeliveredChange;
    }

    void OnCashChange(ulong prevValue, ulong newValue)
    {
        StartCoroutine(CountTo(_cashText, _cashIconText, prevValue, newValue));
    }
    void OnGemChange(ulong prevValue, ulong newValue)
    {
        StartCoroutine(CountTo(_gemText, _gemIconText, prevValue, newValue));
    }
    void OnParcelsDeliveredChange(ulong prevValue, ulong newValue)
    {
        StartCoroutine(CountTo(_parcelsDeliveredText, _parcelsDeliveredIconText, prevValue, newValue));
    }

    void SetText(TextMeshProUGUI _text, TextMeshProUGUI _icon, ulong value)
    {
        _text.text = MathUtil.UlongToString(value, "");
        PlayAnimation(_text.gameObject);
        PlayAnimation(_icon.gameObject);
    }

    void SetIcon(TextMeshProUGUI _text, int _ID)
    {
        _text.text = "<sprite=" + _ID + ">";
    }

    void PlayAnimation(GameObject _gameObj)
    {
        _gameObj.GetComponent<ButtonAnimation>().PlayPulsAnimation();
    }

    IEnumerator CountTo(TextMeshProUGUI text, TextMeshProUGUI iconText, ulong prevValue, ulong newValue)
    {
        ulong start = prevValue;
        ulong score;
        float duration = 0.25f;
        for (float timer = 0; timer < duration; timer += Time.deltaTime)
        {
            float progress = timer / duration;
            score = (ulong)Mathf.Lerp(start, newValue, progress);
            SetText(text, iconText, score);
            yield return null;
        }
        score = newValue;
        SetText(text, iconText, score);
    }
}
