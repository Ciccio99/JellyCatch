using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
       Holds functions that manipulate game objects (This exists so that Hololens voice commands
       willwork even on objects that are loaded into the scene
*/
public class gameObjectManagerScript : MonoBehaviour {

    public float projectileForce = 100f;

    private GameObject player;
    private GameObject netProjectile;

    private void Awake() {
        netProjectile = Resources.Load("Prefabs/NetProjectile") as GameObject;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("MainCamera");
    }

    public void ResetNetPosition() {
        GameObject net = GameObject.Find("Net");
        if (net != null) {
            net.transform.position = player.transform.position + player.transform.forward * 1.2f + player.transform.up * -0.3f;
        }
    }

    public void SpawnNetHarpoon() {
        GameObject net = GameObject.FindGameObjectWithTag("Net");
        GameObject netLauncher = GameObject.FindGameObjectWithTag("NetLauncher");

        if (net != null) {
            Destroy(net);
        }
        if (netLauncher == null) {
        }
    }

    public void FireNetProjectile() {
       GameObject projectile =  Instantiate(netProjectile, Camera.main.transform.position, Quaternion.LookRotation(Camera.main.transform.forward, Camera.main.transform.up));
       Vector3 projVector = (Camera.main.transform.forward * 5 + Camera.main.transform.up).normalized * projectileForce;
       projectile.GetComponent<Rigidbody>().AddForce(projVector);
    }
}
