using UnityEngine;
using System.Collections;

public class PlayerObject : MovableEntity<Entity> {
	
	public int Brains { get; private set; }
	
	public PlayerObject() : base(150) {
		instance = this;
		Brains = 0;
	}
	
	protected override void FixedUpdate () {
		base.FixedUpdate();
		
		float hor = 1.0f; //Input.GetAxis("Horizontal");
		float ver = Input.GetAxis("Vertical");
		
		Vector3 x = new Vector3(hor, 0.0f, ver).normalized;
		rigidbody.AddRelativeForce(x * 8.5f, ForceMode.Impulse);
		
		Brains = 0 + ((int)transform.position.x / 10);
	}
	
	
	
	public readonly Vector3[] Offsets = new Vector3[]{
		new Vector3(1.5f, 0.0f, 1.5f),
		new Vector3(-1.5f, 0.0f, 1.5f),
		new Vector3(1.5f, 0.0f, -1.5f),
		new Vector3(-1.5f, 0.0f, -1.5f),
	};
	
	public Zombie[] Zombies = new Zombie[4];
		
	protected override void Start(){
		base.Start();
		
		for(int i = 0; i < Zombies.Length; i++){
			Zombies[i] = Instantiate("Zombie_Normal", Pos + Offsets[i]).GetComponent<Zombie>();
			Zombies[i].Follow(this, Offsets[i]);
		}
	}
	
	
	public override void Death(){
		base.Death();
		
		//Spiel neu starten
		MessageDispatcher.I.EmptyQueue();
		Application.LoadLevel(Application.loadedLevel);
	}
	
	
	/**
	 * Singleton
	*/
	private static PlayerObject instance;
	public static PlayerObject Instance{get{return instance;}}
	public static PlayerObject I{get{return Instance;}}
	
}
