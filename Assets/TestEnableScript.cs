using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnableScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnEnable()
    {
        Debug.Log("I AMS ENSABLED");
    }

    private void OnDisable()
    {
        Debug.Log("I AM DISABLED :(");
    }
}
