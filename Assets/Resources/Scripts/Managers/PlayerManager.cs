using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public int m_StartingScore = 0;
    public UIManager m_UIManager;
    private int m_PlayerScore;
    
    


    // Use this for initialization
    void Start () {
        m_PlayerScore = m_StartingScore;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void IncrementScore(int value) {
        m_PlayerScore += value;
        m_UIManager.SpawnScoreBubble(m_PlayerScore);
    }

    public int GetCurrentScore() {
        return m_PlayerScore;
    }
}
