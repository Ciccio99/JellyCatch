using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetNetTextScript : MonoBehaviour {

    public float speed = 1f;
    public float distance = 2f;
    public float horizontal = 1f;
    public float vertical = 1f;

    private GameObject player;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("MainCamera");
        transform.position = player.transform.position + player.transform.forward * 2f + player.transform.up * -1.5f;
    }


    // Update is called once per frame
    void Update () {
        Vector3 endPosition = player.transform.position + 
            player.transform.forward * distance + 
            player.transform.up * vertical +
            Vector3.Cross(-player.transform.forward, player.transform.up).normalized * horizontal;
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, endPosition, step);

        transform.LookAt(transform.position + player.transform.rotation * Vector3.forward, Vector3.up);
    }
}
