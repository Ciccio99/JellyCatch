using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jellyPopScript : MonoBehaviour {

    GameObject bubbleBurstPE;
    private bool isFriend;

    private void Awake()
    {
        bubbleBurstPE = Resources.Load("Prefabs/Particle_Effects/BubbleBurstPE") as GameObject;

        isFriend = false;
    }

    public void popJellyfish() {
        int scoreValue = 1;
        if (isFriend) {
            scoreValue = 3;
        }
        ScoreManager.instance.IncrementScore(scoreValue);
        Instantiate(bubbleBurstPE, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void SetFriendStatus(bool status) {
        isFriend = status;
    }
}
