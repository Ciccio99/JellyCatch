using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class scoreBubbleScript : MonoBehaviour {

    public float upSpeed = 0.5f;
    public float amplitude = 1.0f;
    public float destroyTimer = 10.0f;

    private Rigidbody rb;
    private GameObject player;
    private float timePassing;
    private Vector3 startPos;

    // Use this for initialization
    void Start() {
        player = GameObject.FindGameObjectWithTag("MainCamera");

        startPos = player.transform.position + player.transform.forward * 6f + player.transform.up * -2;
        transform.position = startPos;

        Invoke("DestroyThis", destroyTimer);

    }

    // Update is called once per frame
    void Update() {
        BubbleShake();

        BillboardRotation();
    }

    public void SetScoreText(int score)
    {
        transform.GetChild(0).GetComponent<TextMeshPro>().SetText(score.ToString());
    }

     public void SetScoreText(string text)
    {
        transform.GetChild(0).GetComponent<TextMeshPro>().SetText(text);
    }

    private void BillboardRotation() {
        transform.LookAt(transform.position + player.transform.rotation * Vector3.forward, Vector3.up);
    }

    private void BubbleShake() {
        timePassing += Time.deltaTime;

        float sinMod = Mathf.Sin(timePassing) * amplitude;

        transform.position = new Vector3(startPos.x + sinMod, transform.position.y + upSpeed, startPos.z + sinMod);
    }

    private void DestroyThis ()
    {
        Destroy(gameObject);
    }

}
