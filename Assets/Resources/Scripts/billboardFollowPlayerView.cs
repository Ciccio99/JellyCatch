using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class billboardFollowPlayerView : MonoBehaviour {

    public float speed = 1f;
    public float m_DistanceFromPlayer;
    public float m_HeightPositioning;

    private Transform camTran;
    private float m_Step;

    // Use this for initialization
    void Start()
    {
        camTran = Camera.main.transform;
        transform.position = camTran.transform.position + camTran.transform.forward * m_DistanceFromPlayer + camTran.transform.up * -1f;
    }

    private void Update()
    {
        Vector3 endPosition = camTran.transform.position + camTran.transform.forward * m_DistanceFromPlayer + camTran.transform.up * m_HeightPositioning;
        
        m_Step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, endPosition, m_Step);

        transform.LookAt(transform.position + camTran.transform.rotation * Vector3.forward, Vector3.up);
    }

}
