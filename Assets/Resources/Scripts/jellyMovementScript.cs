using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jellyMovementScript : MonoBehaviour {

    public float moveForce = 20.0f;
    public float rotationAngle = 45f;
    public float boundsDistance = 3f;

    protected enum MoveState {ACCELERATE, DECELERATE, ROTATE, STOPPED}

    protected MoveState currState;
    protected Rigidbody rb;
    private Transform mainCamera;

	// Use this for initialization
	protected virtual void Start () {
        rb = gameObject.GetComponent<Rigidbody>();
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        SetRotateState();
	}
	
	// Update is called once per frame
	protected virtual void FixedUpdate () {
        MoveStateLoop();
	}

    protected virtual void MoveStateLoop() {
        switch (currState) {
            case MoveState.ACCELERATE:
                AccelerateJelly();
                break;

            case MoveState.DECELERATE:
                DecelerateJelly();
                CheckIfStopped();
                break;

            case MoveState.ROTATE:
                RotateJelly();
                break;

            case MoveState.STOPPED:
                if (!IsInvoking("SetRotateState")) {
                    Invoke("SetRotateState", Random.Range(0f, 2f));
                }
                break;
        }
    }

    protected virtual void AccelerateJelly() {
        Vector3 forwardForce = transform.forward * moveForce;
        rb.AddForce(forwardForce);
        currState = MoveState.DECELERATE;
    }

    protected virtual void DecelerateJelly() {
        rb.velocity = rb.velocity * 0.95f;
    }

    protected virtual void RotateJelly() {
        rb.angularVelocity *= 0f;

        if (!CheckInBounds()) {
            transform.LookAt((mainCamera.position));
        } else if (RandomRotationRoll()) {
            Vector3 randDirVect = new Vector3(Random.Range(-rotationAngle, rotationAngle), 
                                            Random.Range(-rotationAngle, rotationAngle), 
                                            Random.Range(-rotationAngle, rotationAngle));
            transform.Rotate(randDirVect);
        }

        currState = MoveState.ACCELERATE;
    }

    protected virtual void CheckIfStopped() {
        if (rb.velocity.magnitude <= 0.01f) {
            currState = MoveState.STOPPED;
        }
    }


    /* 
    ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        State Change Functions
    ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    */

    protected virtual void SetAccelerateState() {
        currState = MoveState.ACCELERATE;
    }

    protected virtual void SetRotateState() {
        currState = MoveState.ROTATE;
    }


    /*
     ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        Helper Functions
     ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
     */
    protected virtual bool RandomBool() {
        return Random.value >= 0.5f ? true : false;
    }

    protected virtual bool RandomRotationRoll() {
        return Random.value <= 0.4 ? true : false;
    }

    protected virtual bool CheckInBounds() {
        if ((transform.position - mainCamera.position).magnitude < boundsDistance) {
            return true;
        }

        return false;
    }

}
