using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	void FixedUpdate () {
		float hor = Input.GetAxis("Horizontal");
		float ver = Input.GetAxis("Vertical");
		
		rigidbody.AddRelativeForce(new Vector3(0.0f, 0.0f, 1.0f) * ver * 10.0f, ForceMode.Impulse);
		rigidbody.AddRelativeForce(new Vector3(1.0f, 0.0f, 0.0f) * hor * 10.0f, ForceMode.Impulse);
	}
}
