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
    private GameObject launchNetTextPrefab;
    private bool hasHelped;
    private GameObject helperLaunchObject;
    private bool hasLaunchedProjectile;

    private Hashtable spawnedObjects;

    private void Awake() {
        spawnedObjects = new Hashtable();
        netProjectile = Resources.Load("Prefabs/NetProjectile") as GameObject;
        launchNetTextPrefab = Resources.Load("Prefabs/LaunchNetText") as GameObject;
        hasLaunchedProjectile = false;
        hasHelped = false;
    }

    private void Start()
    {
        player = Camera.main.gameObject;
    }

    private void Update() {
      
    }

    public void ResetNetPosition() {
        GameObject net = GameObject.Find("Net");
        if (net != null) {
            net.transform.position = player.transform.position + player.transform.forward * 1.2f + player.transform.up * -0.3f;
        }
    }

    public void SpawnNetHarpoon() {
        //GameObject netBlaster = Instantiate()
        if (!spawnedObjects.Contains("netBlaster")) {
            spawnedObjects.Add("netBlaster", 1);
        }
    }

    public void FireNetProjectile() {
        if (!hasLaunchedProjectile) {
            hasLaunchedProjectile = true;
            if (helperLaunchObject != null) {
                Destroy(helperLaunchObject);
            }
        }

       GameObject projectile =  Instantiate(netProjectile, Camera.main.transform.position, Quaternion.LookRotation(Camera.main.transform.forward, Camera.main.transform.up));
       Vector3 projVector = (Camera.main.transform.forward * 5 + Camera.main.transform.up).normalized * projectileForce;
       projectile.GetComponent<Rigidbody>().AddForce(projVector);
    }

    public bool HasLaunchedProjectile() {
        return hasLaunchedProjectile;
    }
}
