using UnityEngine;
using System.Collections;

public class PlayerObject : Entity {
	
	public int Brains { get; private set; }
	
	public PlayerObject() : base(150) {
		instance = this;
		Brains = 1337;
	}
	
	void FixedUpdate () {
		float hor = Input.GetAxis("Horizontal");
		float ver = Input.GetAxis("Vertical");
		
		rigidbody.AddRelativeForce(new Vector3(0.0f, 0.0f, 1.0f) * ver * 10.0f, ForceMode.Impulse);
		rigidbody.AddRelativeForce(new Vector3(1.0f, 0.0f, 0.0f) * hor * 10.0f, ForceMode.Impulse);
		Brains = 1337 + ((int)transform.position.x / 10);
	}
	
	
	
	/**
	 * Singleton
	*/
	private static PlayerObject instance;
	public static PlayerObject Instance{get{return instance;}}
	public static PlayerObject I{get{return Instance;}}
	
}
