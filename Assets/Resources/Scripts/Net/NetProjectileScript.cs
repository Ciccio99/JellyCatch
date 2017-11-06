using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetProjectileScript : MonoBehaviour {

	public float m_TimeScale = 2f;
    public float m_TimeUntilDisactive = 6f;
    public float m_StartScaleSize;
    public float m_FinalScaleSize;

	private float m_ScaleProgress;
    private Vector3 m_StartScale;
    private Vector3 m_FinalScale;

    private void Awake()
    {
        m_StartScale = Vector3.one * m_StartScaleSize;
        m_FinalScale = Vector3.one * m_FinalScaleSize;
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        transform.localScale = m_StartScale;
        m_ScaleProgress = 0;
        Invoke("Disactivate", m_TimeUntilDisactive);
    }

    private void OnDisable()
    {
        CancelInvoke("Disactivate");
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.angularVelocity = Vector3.zero;
        rb.velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update () {
		ScaleNetProjectile();
	}

	private void ScaleNetProjectile() {
		transform.localScale = Vector3.Lerp(m_StartScale, m_FinalScale, m_ScaleProgress);
		m_ScaleProgress += Time.deltaTime * m_TimeScale;
	}

	private void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "Jellyfish") {
			col.gameObject.GetComponent<jellyPopScript>().popJellyfish();
		}
	}

	private void DestroySelf() {
		Destroy(gameObject);
	}

    private void Disactivate() {
        gameObject.SetActive(false);
    }
}
