using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class billboardFollowPlayerView : MonoBehaviour {

    public float speed = 1f;

    private GameObject player;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void Update()
    {
        Vector3 endPosition = player.transform.position + player.transform.forward * 5f + player.transform.up * 0.2f;
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, endPosition, step);

        transform.LookAt(transform.position + player.transform.rotation * Vector3.forward, Vector3.up);
    }

}
