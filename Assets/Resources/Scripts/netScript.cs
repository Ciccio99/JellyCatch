using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Handles all scripted interactions for the net object
*/
public class netScript : MonoBehaviour {

    public float timeUntilHelpSpawn = 5f;

    private Vector3 lastPos;
    private GameObject resetNetTextPrefab;
    private GameObject resetNetText;
    
    private void Awake()
    {
        resetNetTextPrefab = Resources.Load("Prefabs/Text/ResetNetText") as GameObject;
    }

    // Use this for initialization
    void Start () {
        lastPos = transform.position;
        Transform player = Camera.main.transform;
        transform.position = player.position + player.forward * 1.2f + player.up * -0.3f;
	}
	
	// Update is called once per frame
	void Update () {
        if (lastPos == transform.position)
        {
            if (!IsInvoking("SpawnNetRespawnText")) {
                Invoke("SpawnNetRespawnText", timeUntilHelpSpawn);
            }
        }
        else {
            if (resetNetText != null) {
                DespawnRespawnText();
            }
        }
        lastPos = transform.position;
	}

    void SpawnNetRespawnText() {
        if (resetNetText == null) {
            Transform camera = Camera.main.transform;
            resetNetText = Instantiate(resetNetTextPrefab, camera.position, Quaternion.identity);
        } 
    }

    void DespawnRespawnText() {
        Destroy(resetNetText);
        resetNetText = null;
 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Jellyfish")
        {
            collision.gameObject.GetComponent<jellyPopScript>().popJellyfish();
        }
    }
}
