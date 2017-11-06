using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RudeJellyScript : jellyMovementScript {

	public float timeUntilDeath = 15f;
	public int pointsNegated = -2;

	private Vector3 ogPlayerDir;
	private bool hasCollided;
	private GameObject electricShoockScreenPE;

	private void Awake() {
		electricShoockScreenPE = Resources.Load("Prefabs/Particle_Effects/ElectricShockScreenPE") as GameObject;
	}

	// Use this for initialization
	protected override void Start () {
		rb = gameObject.GetComponent<Rigidbody>();
		ogPlayerDir = (Camera.main.transform.position - transform.position).normalized;
		base.SetRotateState();
		Invoke("DestroySelf", timeUntilDeath);
		hasCollided = false;
	}
	
	// Update is called once per frame
	protected override void FixedUpdate () {
		base.FixedUpdate();
	}

	protected override void RotateJelly() {
		rb.angularVelocity *= 0f;
		transform.rotation = Quaternion.LookRotation(ogPlayerDir);
		currState = MoveState.ACCELERATE;
	}

	private void DestroySelf() {
		Destroy(gameObject);
	}

	void OnTriggerEnter(Collider col) {
		if (!hasCollided && col.gameObject.tag == "MainCamera") {
			hasCollided = true;
			ScoreManager.instance.IncrementScore(pointsNegated);
			SpawnElectrickShockScreen();
		}
	}

	private void SpawnElectrickShockScreen() {
		GameObject effect = Instantiate(electricShoockScreenPE, Camera.main.transform);
		effect.transform.position = Camera.main.transform.forward * 2f;
	}

}
