using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class netProjectileScript : MonoBehaviour {

	public float timeScale = 2f;
	private float scaleProgress;
	private Vector3 startScale = new Vector3(0, 0, 0);
	private Vector3 finalScale = new Vector3(0.7f, 0.7f, 0.7f);

	// Use this for initialization
	void Start () {
		Invoke("DestroySelf", 6f);
		transform.localScale = startScale;
		scaleProgress = 0;
	}
	
	// Update is called once per frame
	void Update () {
		ScaleNetProjectile();
	}

	private void ScaleNetProjectile() {
		transform.localScale = Vector3.Lerp(startScale, finalScale, scaleProgress);
		scaleProgress += Time.deltaTime * timeScale;
	}

	private void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "Jellyfish") {
			col.gameObject.GetComponent<jellyPopScript>().popJellyfish();
		}
	}

	private void DestroySelf() {
		Destroy(gameObject);
	}
}
