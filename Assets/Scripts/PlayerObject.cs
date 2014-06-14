﻿using UnityEngine;
using System.Collections;

public class PlayerObject : MovableEntity<Entity> {



	public PlayerObject() : base(150) {}



	//Gehirnzähler mit Observer
	private int brains = 0;
	public int Brains {
		get{ return brains; }
		set { 
			brains = value;
			Observer.I.Update("Brains", Brains);
		}
	}



	protected override void FixedUpdate () {
		//Tastatur
		float ver = Input.GetAxis("Vertical");
		Vector3 x = new Vector3(0f, 0f, ver) * MaxSpeed;
		Steering.TargetPos = Pos + x;

		base.FixedUpdate();

		//konstante Bewegung nach rechts
		rigidbody.AddForce(new Vector3(MaxSpeed, 0f, 0f), ForceMode.Impulse);
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

		instance = this;

		MaxSpeed = 2.4f;
		MaxForce = 2.4f;

		Steering.Seeking = true;
		Steering.f_SeekFactor = 1.0f;
		Steering.Wandering = true;
		Steering.f_WanderFactor = 0.25f;

		Brains = 0;
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
				//momentan aktive beschleunigungseffekte übertragen
				z.SpeedBonus += SpeedBonus;
				return;
			}
		}
		//kein freier Platz
		//nichts tun
	}

	public void Eat(Human h){
		Brains++;
		h.Death();
		PlaySound("RCL/omnomnom");
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
