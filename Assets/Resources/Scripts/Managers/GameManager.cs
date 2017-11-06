using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance { set; get; }
    public string startSceneName = "StartScene";
    public string gameSceneName = "GameScene";

    public enum GameState {START, SCANNING, START_SCENE, PLAYING};
    private GameState m_CurrentState;


    private void Awake()
    {
        if (instance == null) {
            instance = this;
        }
    }

    // Use this for initialization
    private void Start () {
        m_CurrentState = GameState.START;
        StartCoroutine(GameLoop());
	}

    public void StartGame() {
        if (m_CurrentState == GameState.START_SCENE)
            m_CurrentState = GameState.PLAYING;
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

    public string GetCurrentSceneName() {
        return SceneManager.GetActiveScene().name;
    }

    public GameState GetGameState() {
        return m_CurrentState;
    }

    private IEnumerator GameLoop() {

        // Start by scanning the room and gathering mesh data
        yield return StartCoroutine(ScanRoom());
        yield return StartCoroutine(GamePlaying());
        yield return StartCoroutine(GameLoop());
    }

    private IEnumerator ScanRoom() {
        m_CurrentState = GameState.SCANNING;
        yield return StartCoroutine(JellySpatialMappingManager.instance.StartRoomScan());
    }

    private IEnumerator GamePlaying() {
        m_CurrentState = GameState.START_SCENE;
        Debug.Log("Starting start scene now");
        LoadScene(startSceneName);

        while(m_CurrentState != GameState.PLAYING) {
            yield return null;
        }
        Debug.Log("Starting Playing scene now");
        UnloadScene(startSceneName);
        LoadScene(gameSceneName);
        while(m_CurrentState == GameState.PLAYING) {
            yield return null;
        }
    }

}
