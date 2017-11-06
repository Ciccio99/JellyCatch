using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR.WSA;

public class JellySpatialMappingManager : MonoBehaviour {

	public static JellySpatialMappingManager instance {get; set;}

	public SpatialMappingRenderer m_SpatialMappingRenderer;
	public SpatialMappingCollider m_SpatialMappingCollider;
	public GameObject m_ScanningTextGO;

	public GameObject m_SpatialMappingVisualizer;
	
	public Vector3 m_LowestPointOnSpatialMap;

	public Material m_SandMaterial;
	public Material m_ScanningMaterial;

	private bool m_IsCurrentlyScanning = false;

	private void Awake() {
		if (instance == null) {
			instance = this;
		}

		m_LowestPointOnSpatialMap = Vector3.zero;
	}

	private void OnEnable() {
		// Don't start scanning the room until explicitly told to
		SetRendererFreezeUpdates(true);
		SetColliderFreezeUpdates(true);
		m_ScanningTextGO.SetActive(false);
		m_SpatialMappingRenderer.visualMaterial = m_ScanningMaterial;
	}

	public void StopScanningRoom() {
		if (GameManager.instance.GetGameState() == GameManager.GameState.SCANNING){
			m_IsCurrentlyScanning = false;
		}
	}
	
	public IEnumerator StartRoomScan() {
		m_IsCurrentlyScanning = true;
		m_SpatialMappingRenderer.visualMaterial = m_ScanningMaterial;
		m_ScanningTextGO.SetActive(true);
		
		// Enable scanning
		SetRendererFreezeUpdates(false);
		SetColliderFreezeUpdates(false);

		// Keep scanning until IsCurrentlyScanning is set to false
		while (m_IsCurrentlyScanning) {
			yield return null;
		}
		// Stopping scan
		m_ScanningTextGO.SetActive(false);
		SetRendererFreezeUpdates(true);
		SetColliderFreezeUpdates(true);

		ProcessSpatialMapMeshes();
		SetSandMaterialHeightPoint(m_LowestPointOnSpatialMap.y);
		// Applyiong sand texture
		ApplySpatialMapMaterial(m_SandMaterial);
		
	}

	private void ProcessSpatialMapMeshes() {
		foreach(Transform child in m_SpatialMappingVisualizer.transform) {
			MeshFilter mf = child.GetComponent<MeshFilter>();
			mf.mesh.RecalculateNormals();
			GetLowestBoundingPoint(child, mf.sharedMesh.vertices);
		}
	}

	private void GetLowestBoundingPoint(Transform meshTransform, Vector3[] vertices) {
		foreach(Vector3 vert in vertices) {
			Vector3 vertWorld = meshTransform.localToWorldMatrix.MultiplyPoint3x4(vert);
			if (vertWorld.y < m_LowestPointOnSpatialMap.y) {
				m_LowestPointOnSpatialMap = vertWorld;
			}
		}
	}

	private void ApplySpatialMapMaterial(Material mat) {
		m_SpatialMappingRenderer.visualMaterial = mat;
		MeshRenderer[] renderers = m_SpatialMappingVisualizer.GetComponentsInChildren<MeshRenderer>();
		foreach(MeshRenderer rend in renderers) {
			rend.material = mat;
		}
	}

	private void SetSandMaterialHeightPoint(float value) {
		m_SandMaterial.SetFloat("_ElevationPoint", value);
	}


	private void SetRendererFreezeUpdates(bool value) {
		m_SpatialMappingRenderer.freezeUpdates = value;
	}

	private void SetColliderFreezeUpdates(bool value) {
		m_SpatialMappingCollider.freezeUpdates = value;
	}

	private void CalculateSpatialMapBounds() {
		Bounds combinedBounds = m_SpatialMappingCollider.GetComponent<Collider>().bounds;
		Collider[] childrenColliders = m_SpatialMappingCollider.GetComponentsInChildren<Collider>();
		foreach(Collider col in childrenColliders) {
			combinedBounds.Encapsulate(col.bounds);
		}
		
	}
}
