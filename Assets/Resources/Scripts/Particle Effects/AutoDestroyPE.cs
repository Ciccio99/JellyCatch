﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyPE : MonoBehaviour {

    private ParticleSystem ps;

	// Use this for initialization
	void Start () {
        ps = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!ps.IsAlive()) {
            Destroy(gameObject);
        }
	}
}
