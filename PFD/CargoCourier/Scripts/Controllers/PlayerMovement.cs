using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerMovement : GameElement
{
    public event Action<bool> ON_PLAYER_WALK;

    public Joystick joystick;

    float angleBody;
    private PlayerView playerView;


    private void Start()
    {
        GetPlayerView(app.view.GetPlayerView());

        app.model.currentCityModel.ON_CONFIG_CHANGED += CurrentCityChenged;
    }

    private void CurrentCityChenged(CityModel _cityModel)
    {
        transform.position = _cityModel.GetAirportPlayerPosition();
    }

    public void GetPlayerView(PlayerView _view)
    {
        playerView = _view;
    }

    public void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        if (joystick.IsTouched)
        {
            ON_PLAYER_WALK?.Invoke(true);
            angleBody = Mathf.Atan2(joystick.Horizontal, joystick.Vertical) * Mathf.Rad2Deg;
            playerView.GetBody().eulerAngles = new Vector3(0f, angleBody, 0f);

            Vector3 direction = transform.forward * joystick.Vertical + transform.right * joystick.Horizontal;
            transform.position += direction.normalized * Time.deltaTime * app.view.GetPlayerView().model.GetSpeed();
        }
        else ON_PLAYER_WALK?.Invoke(false);
    }
}