using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetLaunching : MonoBehaviour{

    
    public AudioSource m_NetAudio;
    public AudioClip m_NetChargeClip;
    public AudioClip m_NetLaunchClip;
    public AudioClip m_NetReelClip;
    public Rigidbody m_Net;
    public Transform m_LaunchTransform;
    public GameObject m_NetProjectile;
    public float m_MaxLaunchForce = 100f;
    public float m_MinLaunchForce = 20f;
    public float m_MaxChargeTime = 1.0f;

    private float m_CurrentLaunchForce;
    private float m_ChargeSpeed;
    private bool m_Launched;

    // Use this for initialization
    void Start()
    {
        m_ChargeSpeed = (m_MaxLaunchForce - m_MinLaunchForce) / m_MaxChargeTime;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChargeNetCast() { }

    public void CastNet() {
        m_NetProjectile.SetActive(false);
        m_NetProjectile.transform.rotation = transform.rotation;
        m_NetProjectile.transform.position = m_LaunchTransform.position;
        m_NetProjectile.SetActive(true);
        m_NetProjectile.GetComponent<Rigidbody>().AddForce(m_MaxLaunchForce * m_LaunchTransform.forward);
    }

    public void ReelNet() { }


    public void NetInputTap()
    {
        CastNet();
    }
}
