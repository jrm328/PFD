﻿using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonAnimation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private bool _depensOnInteactability = true;
    private Button _buttonCached;
    private bool _isButtonIntaractable
    {
        get
        {
            if (_buttonCached == null)
            {
                _buttonCached = GetComponent<Button>();
            }
            if (_buttonCached == null)
            {
                return false || !_depensOnInteactability;
            }
            return _buttonCached.interactable;
        }
    }
    [SerializeField]
    private float _animationDurationIn = .05f;
    [SerializeField]
    private float _animationDurationOut = .15f;
    [SerializeField]
    private float _pulsDuration = .1f;
    [SerializeField]
    private float _toScaleIn = .9f;
    [SerializeField]
    private float _toScaleOut = 1.2f;
    [SerializeField]
    private float _amplitude = 1f;
    private RectTransform _rectTransform;

    void Awake()
    {
        _rectTransform = transform as RectTransform;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_isButtonIntaractable) return;
        _rectTransform?.DOKill();
        _rectTransform?.DOScale(Vector3.one * _toScaleIn, _animationDurationIn).SetEase(Ease.InOutQuad);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!_isButtonIntaractable) return;
        _rectTransform?.DOKill();
        _rectTransform?.DOScale(Vector3.one, _animationDurationOut).SetEase(Ease.OutBack, _amplitude);
    }

    public void PlayPulsAnimation()
    {
        _rectTransform?.DOKill();
        if (_rectTransform != null) _rectTransform.localScale = Vector3.one;
        _rectTransform?.DOScale(Vector3.one * _toScaleOut, _pulsDuration).SetEase(Ease.InOutQuad).OnComplete(PlayPulsAnimationOut);
    }

    void PlayPulsAnimationOut()
    {
        _rectTransform?.DOKill();
        _rectTransform?.DOScale(Vector3.one, _pulsDuration).SetEase(Ease.OutBack, _amplitude);
    }

    void OnEnable()
    {
        PlayPulsAnimation();
    }
}
