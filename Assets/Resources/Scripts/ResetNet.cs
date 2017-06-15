using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetNet : MonoBehaviour {

    Transform mainCam;

    private void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    public void ResetNetPosition() {
        transform.position = mainCam.position + mainCam.forward * 1.3f;
    }
}
