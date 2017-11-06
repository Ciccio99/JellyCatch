using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.SpatialMapping;

public class RecalculateSurfaceNormals : MonoBehaviour {

	public float m_TimeBetweenUpdates;
	private MeshFilter[] m_SurfaceMeshes;

	void FixedUpdate() {
		if (!IsInvoking("CalculateNormals")) 
			Invoke("CalculateNormals", m_TimeBetweenUpdates);
	}

	private void CalculateNormals() {
		m_SurfaceMeshes = GetComponentsInChildren<MeshFilter>();
		if (m_SurfaceMeshes.Length > 0) {
			foreach(MeshFilter mf in m_SurfaceMeshes) {
				mf.mesh.RecalculateNormals();
			}
		}
		
	}
}
