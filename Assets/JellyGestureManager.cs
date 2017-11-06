using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR.WSA.Input;

public class JellyGestureManager : MonoBehaviour {

	public gameObjectManagerScript m_GOManager;
	public PlayerNetLaunching m_PlayerNetLaunching;

	private GestureRecognizer m_GestureRecognizer;

	// Use this for initialization
	void Start () {
		// Gesture recognizer holds the delegate events that functions can subscribe to
		m_GestureRecognizer = new GestureRecognizer();
		m_GestureRecognizer.StartCapturingGestures();

		m_GestureRecognizer.TappedEvent += OnTappedEvent;
	}

	/// <Summary>
	/// Activates varied tap related functions based on the current GameState
	/// <Summary>
	private void OnTappedEvent(InteractionSourceKind source, int tapCount, Ray headRay) {
		switch(GameManager.instance.GetGameState()) {
			case GameManager.GameState.SCANNING:
				JellySpatialMappingManager.instance.StopScanningRoom();
			break;
			
			case GameManager.GameState.START_SCENE:
				GameManager.instance.StartGame();
			break;

			case GameManager.GameState.PLAYING:
				m_PlayerNetLaunching.NetInputTap();
			break;
		}
	}
}
