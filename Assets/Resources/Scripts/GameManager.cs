using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance { set; get; }
    public static string startSceneName = "StartScene";
    public static string gameSceneName = "GameScene";

    private enum GameState { START, GAME }  
    private GameState currState;



    private void Awake()
    {
        if (instance == null) {
            instance = this;
        }
    }

    // Use this for initialization
    private void Start () {
        currState = GameState.START;
        LoadScene(startSceneName);
	}

    public void StartGame() {
        if (currState == GameState.START) {
            currState = GameState.GAME;
            UnloadScene(startSceneName);
            LoadScene(gameSceneName);
        }
    }

    public void LoadScene(string sceneName) {
        if (!SceneManager.GetSceneByName(sceneName).isLoaded) {
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        }
    }

    public void UnloadScene(string sceneName) {
        if (SceneManager.GetSceneByName(sceneName).isLoaded) {
            SceneManager.UnloadSceneAsync(sceneName);
        }
    }

}
