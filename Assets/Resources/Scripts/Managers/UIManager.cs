using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour {

    public static UIManager instance { get; set; }

    private GameObject scoreBubblePrefab;
    private GameObject negativeScoreBubblePrefab;
    private GameObject textBubblePrefab;
    private TextMeshProUGUI scoreText;
    private GameObject holoHUDGameUI;
    private GameObject holoHUDStartUI;

    private void Awake()
    {
        if (instance == null) {
            instance = this;
        }

        scoreBubblePrefab = Resources.Load("Prefabs/ScoreBubble") as GameObject;
        negativeScoreBubblePrefab = Resources.Load("Prefabs/NegativeScoreBubble") as GameObject;
        textBubblePrefab = Resources.Load("Prefabs/TextBubble") as GameObject;
    }

    public void SpawnScoreBubble(int value) {
        GameObject bubble = Instantiate(scoreBubblePrefab);
        bubble.GetComponent<scoreBubbleScript>().SetScoreText(value);
    }

    public void SpawnTextBubble(string text) {
        GameObject bubble = Instantiate(textBubblePrefab);
        bubble.GetComponent<scoreBubbleScript>().SetScoreText(text); 
    }

    public void SpawnNegativeScoreBubble(int value) {
        GameObject bubble = Instantiate(negativeScoreBubblePrefab);
        bubble.GetComponent<scoreBubbleScript>().SetScoreText(value);      
    }

    public void SpawnNegativeScoreBubble(string text) {
        GameObject bubble = Instantiate(negativeScoreBubblePrefab);
        bubble.GetComponent<scoreBubbleScript>().SetScoreText(text);      
    }  
}
