using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using System;

public class NetTapInputRecognizer : MonoBehaviour, IInputClickHandler {

    public PlayerNetLaunching m_PlayerNetLaunching;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        GameManager.GameState state = GameManager.instance.GetGameState();

        switch(state){
            case GameManager.GameState.SCANNING:
                JellySpatialMappingManager.instance.StopScanningRoom();
                break;
            case GameManager.GameState.PLAYING:
                m_PlayerNetLaunching.NetInputTap();
                break;
        }
    }
}
