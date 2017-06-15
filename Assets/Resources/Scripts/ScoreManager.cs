using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

	public static ScoreManager instance {get; set;}

	private int playerScore;
	private int scoreMultiplier;
	private int jellyCatchCount;

	private void Awake() {
		if (instance == null) {
			instance = this;
		}
	}
	// Use this for initialization
	void Start () {
		playerScore = 0;
		scoreMultiplier = 1;
	}

	public void IncrementScore(int value)
    {
        playerScore += value * scoreMultiplier;
        if (value < 0 ) {
            UIManager.instance.SpawnNegativeScoreBubble(value);
            Invoke("SpawnScoreBubble", 1f);
        } else {
            UIManager.instance.SpawnScoreBubble(playerScore);
        }
		CheckMultiplier();
        Invoke("ResetMultiplier", 5f);
    }

	  public void ResetScore()
    {
        playerScore = 0;
    }

	private void CheckMultiplier() {
		jellyCatchCount++;

		if (jellyCatchCount % 5 == 0) {
			scoreMultiplier++;
			Invoke("DisplayMultiplier", 0.5f);
		}
		CancelInvoke("ResetMultiplier");
	}

	private void DisplayMultiplier() {
		UIManager.instance.SpawnTextBubble(scoreMultiplier.ToString() + "x");
	}

	private void ResetMultiplier() {
		if (scoreMultiplier > 1) {
			UIManager.instance.SpawnNegativeScoreBubble("1x");
			scoreMultiplier = 1;
		}
		if (jellyCatchCount > 0) {
			jellyCatchCount = 0;
		}
	}
}
