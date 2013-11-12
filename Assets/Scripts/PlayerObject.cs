using UnityEngine;
using System.Collections;

public class PlayerObject : MovableEntity<Entity> {
	
	public int Brains { get; private set; }
	
	public PlayerObject() : base(150) {
		instance = this;
		Brains = 1337;
	}
	
	protected override void FixedUpdate () {
		base.FixedUpdate();
		
		float hor = Input.GetAxis("Horizontal");
		float ver = Input.GetAxis("Vertical");
		
		rigidbody.AddRelativeForce(new Vector3(0.0f, 0.0f, 1.0f) * ver * 10.0f, ForceMode.Impulse);
		rigidbody.AddRelativeForce(new Vector3(1.0f, 0.0f, 0.0f) * hor * 10.0f, ForceMode.Impulse);
		Brains = 1337 + ((int)transform.position.x / 10);
	}
	
	
	
	public readonly Vector3[] Offsets = new Vector3[]{
		new Vector3(1.0f, 0.98f, 1.0f),
		new Vector3(-1.0f, 0.98f, 1.0f),
		new Vector3(1.0f, 0.98f, -1.0f),
		new Vector3(-1.0f, 0.98f, -1.0f),
	};
	
	public Zombie[] Zombies = new Zombie[4];
		
	protected override void Start(){
		base.Start();
		
		for(int i = 0; i < Zombies.Length; i++){
			Zombies[i] = Instantiate("Zombie_Normal", Pos + Offsets[i]).GetComponent<Zombie>();
			Zombies[i].Follow(this, Offsets[i]);
		}
	}
	
	
	/**
	 * Singleton
	*/
	private static PlayerObject instance;
	public static PlayerObject Instance{get{return instance;}}
	public static PlayerObject I{get{return Instance;}}
	
}
