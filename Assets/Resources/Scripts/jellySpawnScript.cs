using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jellySpawnScript : MonoBehaviour {
    public int maxNumOfJelly = 30;
    public int numOfJellyToSpawn = 20;
    public float timeBetweenJellySpawns = 5f;
    public float maxPos = 5;

    private Transform mainCamTransform;
    private GameObject jellyPrefab;
    private GameObject friendJelly;
    private GameObject rudeJelly;
    private GameObject spatialMapping;
    private bool hasInitSpawned;

    private void Awake()
    {
        jellyPrefab = Resources.Load("Prefabs/Jellyfish") as GameObject;
        friendJelly = Resources.Load("Prefabs/Friend_Jellyfish") as GameObject;
        rudeJelly = Resources.Load("Prefabs/Rude_Jellyfish") as GameObject;
    }

    void Start () {

        mainCamTransform = Camera.main.transform;

        hasInitSpawned = false;
	}

    private void Update()
    {
        if (spatialMapping == null) {
            spatialMapping = GameObject.Find("Surface Parent0");
        } else {
            if (!hasInitSpawned)
            {
                SpawnJellyfish(numOfJellyToSpawn);
                hasInitSpawned = true;
            }

            if (GameObject.FindGameObjectsWithTag("Jellyfish").Length < maxNumOfJelly)
            {
                if (!IsInvoking("SpawnJellyfish")) {
                    Invoke("SpawnJellyfish", timeBetweenJellySpawns);
                }
            }
            // Removing enemy jellyfish for now becauses it was a meh mechanic
            // if (GameObject.Find("Rude_Jellyfish(Clone)") == null && !IsInvoking("SpawnRudeJellyfish")) {
            //     Invoke("SpawnRudeJellyfish", Random.Range(5f, 15f));
            // }
        }       
    }

    private void SpawnRudeJellyfish() {
        Instantiate(rudeJelly, getSpawnPos(4f), Quaternion.identity);
    }

    public void SpawnJellyfish() {
        if (IsFriendSpawning())
        {
            GameObject jelly = Instantiate(friendJelly, getSpawnPos(), Quaternion.identity);
            jelly.GetComponent<jellyPopScript>().SetFriendStatus(true);
        }
        else
        {
            Instantiate(jellyPrefab, getSpawnPos(), Quaternion.identity);
        }
    }

    public void SpawnJellyfish(int numToSpawn) {
        for (int i = 0; i < numToSpawn; i++)
        {
            SpawnJellyfish();
        }
    }

    private Vector3 getSpawnPos() {
        Vector3 spawnPos;
        Vector3 randDir = new Vector3(Random.Range(-1f, 1f), Random.Range(0f, 1f), Random.Range(-1f, 1f));
        RaycastHit[] hits;

        hits = Physics.RaycastAll(mainCamTransform.position, randDir, maxPos);

        for (int i = 0; i < hits.Length; i++) {
            GameObject hitObj = hits[i].transform.gameObject;
            if (hitObj.transform.root.name == "Surface Parent0") {
                spawnPos = (hits[i].point - mainCamTransform.position).normalized * Random.Range(1.0f, Mathf.Floor(hits[i].distance));
                return spawnPos;
            }
        }

        spawnPos = (randDir - mainCamTransform.position).normalized * Random.Range(1.0f, maxPos / 2);

        return spawnPos;
    }

    /*
        Overloaded getSpawnPos that allows you to set the minimum distance of a spawn location
        @param minPos | minimum distance from player
        @return Spawn position Vector3
     */
     private Vector3 getSpawnPos(float minPos) {
        Vector3 spawnPos;
        Vector3 randDir = new Vector3(Random.Range(-1f, 1f), Random.Range(0f, 1f), Random.Range(-1f, 1f));
        RaycastHit[] hits;

        hits = Physics.RaycastAll(mainCamTransform.position, randDir, maxPos);

        for (int i = 0; i < hits.Length; i++) {
            GameObject hitObj = hits[i].transform.gameObject;
            if (hitObj.transform.root.name == "Surface Parent0") {
                spawnPos = (hits[i].point - mainCamTransform.position).normalized * Random.Range(minPos, Mathf.Floor(hits[i].distance));
                return spawnPos;
            }
        }

        spawnPos = (randDir - mainCamTransform.position).normalized * Random.Range(minPos, maxPos / 2);

        return spawnPos;
    }

    private bool IsFriendSpawning() {
        return Random.Range(0f, 10f) > 9 ? true : false;
    }

    private bool IsEnemySpawning() {
        return Random.Range(0f, 10f) > 7 ? true : false;
    }
}
