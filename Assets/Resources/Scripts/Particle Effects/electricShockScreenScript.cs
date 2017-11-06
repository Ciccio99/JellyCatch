using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class electricShockScreenScript : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2f;
	}
}
