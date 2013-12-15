using UnityEngine;
using System.Collections;

public class PlayerObject : MovableEntity<Entity> {
	
	public int Brains { get; private set; }
	
	public PlayerObject() : base(150) {
		instance = this;
		Brains = 0;
	}


	protected override void FixedUpdate () {
		float hor = 1.0f; //Input.GetAxis("Horizontal");
		float ver = Input.GetAxis("Vertical");
		Vector3 x = new Vector3(hor, 0.0f, ver).normalized * MaxSpeed;
		Steering.TargetPos = Pos + x;

		base.FixedUpdate();
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

		MaxSpeed = 2.5f;
		MaxForce = 2.5f;

		Steering.Seeking = true;
		Steering.f_SeekFactor = 1.0f;
		Steering.Wandering = true;
		Steering.f_WanderFactor = 0.25f;
	}

	public void AddToGroup(Zombie z){
		//nicht erneut hinzufügen
		foreach(Zombie other in Zombies){
			if(z == other) return;
		}
		//Suche freien Offset Platz
		for(int i = 0; i < Zombies.Length; i++){
			//Freier Platz gefunden
			if(Zombies[i] == null){
				//Eintragen
				Zombies[i] = z;
				//informieren
				z.Follow(this, Offsets[i]);
				return;
			}
		}
		//kein freier Platz
		//nichts tun
	}

	public void Eat(Human h){
		Brains++;
		h.Death();
		PlaySound("RCL/01_05");
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
