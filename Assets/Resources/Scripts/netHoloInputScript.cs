using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using System;

public class netHoloInputScript : MonoBehaviour, IManipulationHandler {

    private Vector3 currHandPos;

    private IInputSource currentInputSource = null;
    private uint currentInputSourceId;

    public void OnManipulationCanceled(ManipulationEventData eventData)
    {
        throw new NotImplementedException();
    }

    public void OnManipulationCompleted(ManipulationEventData eventData)
    {
        throw new NotImplementedException();
    }

    public void OnManipulationStarted(ManipulationEventData eventData)
    {
        Debug.Log("Starting track of THING:");
        Debug.Log(eventData.InputSource.ToString());

        if (currentInputSource == null) {
            currentInputSource = eventData.InputSource;
            currentInputSourceId = eventData.SourceId;
        }
    }

    public void OnManipulationUpdated(ManipulationEventData eventData)
    {
        Debug.Log("Updating THING:");
        Debug.Log(eventData.InputSource.ToString());
        if (currentInputSource != null) {
            eventData.InputSource.TryGetPosition(currentInputSourceId, out currHandPos);
            gameObject.transform.position = currHandPos;
        }
    }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    
}
