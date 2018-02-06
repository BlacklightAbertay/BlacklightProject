using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class move : MonoBehaviour {

	public float force = 5.0f;

	Rigidbody rb;

	Vector2 thumbstickPos;

	private void Awake() {
		InteractionManager.InteractionSourceUpdated += InteractionManager_InteractionSourceUpdated;
	}

	// Use this for initialization
	void Start() {
		rb = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	/*void FixedUpdate() {
		if (Input.GetKeyDown(KeyCode.Space)) {
			Debug.Log("d");
			//Vector3 direction = transform.forward * cam.transform.rot;
			rb.AddForce(transform.forward * force, ForceMode.Impulse);
		}
	}*/

	public void PlayerMove() {
		Debug.Log("ThumbstickPos: " + thumbstickPos);
		Vector3 moveDirection = new Vector3(thumbstickPos.x, 0.0f, thumbstickPos.y);
		moveDirection = moveDirection.normalized;
		float moveAmount = thumbstickPos.magnitude;
		GameObject leftHand = GameObject.FindGameObjectWithTag("LeftHand");
		moveDirection = leftHand.transform.rotation * moveDirection;
		//moveDirection.Scale(leftHand.transform.forward);
		if (leftHand != null) {
			Quaternion handRotation = leftHand.transform.rotation;
			Debug.Log(handRotation);
			if (moveAmount > 0.3) {
				rb.velocity = moveDirection * force * moveAmount * Time.deltaTime;
				//rb.AddForce(handRotation * transform.forward * moveAmount * 10.0f);
			}
			else {
				rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
			}
		}
	}

private void InteractionManager_InteractionSourceUpdated(InteractionSourceUpdatedEventArgs obj) {
		thumbstickPos = obj.state.thumbstickPosition;
		PlayerMove();
	}
}