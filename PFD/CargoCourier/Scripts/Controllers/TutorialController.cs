using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialController: GameElement
{
    public SenderView senderView;
    public ReceiverView receiverView;

    public bool _isTouched;
    public bool _isGetParcel;

    Camera _camera;

    public float noteOffsetY;
    public float noteBOffsetY;

    private void OnEnable()
    {
        app.view.tutorialHud.noteB.SetActive(false);

        app.controller.ON_TUTORIAL_CHANGED += OnTutorialChange;
        app.controller.ON_PLAYER_GET_PARCEL += OnPlayerGetParcel;

        TutorialCanvasVisibility(app.controller.IsTutorial);

        noteOffsetY = 2.5f;
        noteBOffsetY = 30f;
    }

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (app.controller.IsTutorial)
        {
            if (Input.GetMouseButtonDown(0) && !_isTouched)
            {
                _isTouched = true;
                HideTutorialFinger();
            }

            Vector2 _notePosition = _camera.WorldToScreenPoint(senderView.gameObject.transform.position + new Vector3(0f, noteOffsetY, 0f));
            app.view.tutorialHud.note.transform.position = _notePosition;

            Vector2 _noteBPosition = receiverView.GetUIParcelIcon().transform.position + new Vector3(0f, noteBOffsetY, 0f);
            app.view.tutorialHud.noteB.transform.position = _noteBPosition;
        }
    }

    void OnPlayerGetParcel(ParcelConfig parcelConfig)
    {
        if(app.controller.IsTutorial && !_isGetParcel)
        {
            _isGetParcel = true;
            app.view.tutorialHud.note.SetActive(false);
            app.view.tutorialHud.noteB.SetActive(true);
        }
    }

    void OnTutorialChange(bool value)
    {
        TutorialCanvasVisibility(value);
    }

    public void TutorialCanvasVisibility(bool value)
    {
        app.view.tutorialHud.canvasTutorial.SetActive(value);
    }
    void HideTutorialFinger()
    {
        app.view.tutorialHud.fingerIcon.SetActive(false);
    }
}