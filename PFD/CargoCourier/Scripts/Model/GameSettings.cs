using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : GameElement
{
    [Header("Target Frame Rate")]
    public int targetFrameRate = 60;

    [Header("Show Cheat Panel")]
    public bool showCheatPanel;

    [Header("Show Tutorial")]
    public bool showTutorial;

    [Header("Test Device In Any Case")]
    public bool testDeviceAnyCase;

    [Header("English In Any Case")]
    public bool englishInAnyCase;

    [Header("Test Ads Any Case")]
    public bool testAdsInAnyCase;

    [Header("Spawn Enemies")]
    public bool spawnEnemies;



    private void Awake()
    {
        Application.targetFrameRate = targetFrameRate;
    }

}
